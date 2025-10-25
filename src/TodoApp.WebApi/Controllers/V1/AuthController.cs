
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Abstractions;
using TodoApp.Application.Auth.DTOs;
using TodoApp.Domain.Users;
using TodoApp.Infrastructure.Identity;
using TodoApp.Infrastructure.Persistence;

namespace TodoApp.WebApi.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/auth")]
public sealed class AuthController(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    ApplicationContext db,
    IJwtTokenService tokens) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult> Register(RegisterRequest request, CancellationToken ct)
    {
        var user = new AppUser { Id = Guid.NewGuid(), Email = request.Email, UserName = request.Email };
        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        if (!await roleManager.RoleExistsAsync("User"))
            await roleManager.CreateAsync(new AppRole { Name = "User" });
        await userManager.AddToRoleAsync(user, "User");

        return Ok();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenResponse>> Login(LoginRequest request, CancellationToken ct)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email, ct);
        if (user is null) return Unauthorized();

        if (!await userManager.CheckPasswordAsync(user, request.Password))
            return Unauthorized();

        var roles = await userManager.GetRolesAsync(user);
        var (access, expires) = tokens.CreateAccessToken(user, roles);
        var refresh = tokens.CreateRefreshToken();

        db.Set<RefreshToken>().Add(new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refresh,
            ExpiresUtc = DateTime.UtcNow.AddDays(7)
        });
        await db.SaveChangesAsync(ct);

        return new TokenResponse(access, expires, refresh);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenResponse>> Refresh([FromBody] string refreshToken, CancellationToken ct)
    {
        var rt = await db.Set<RefreshToken>().FirstOrDefaultAsync(x => x.Token == refreshToken && !x.Revoked, ct);
        if (rt is null || rt.ExpiresUtc < DateTime.UtcNow) return Unauthorized();

        var user = await userManager.FindByIdAsync(rt.UserId.ToString());
        if (user is null) return Unauthorized();

        rt.Revoked = true;
        var newToken = Guid.NewGuid().ToString("N");
        db.Set<RefreshToken>().Add(new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = newToken,
            ExpiresUtc = DateTime.UtcNow.AddDays(7),
            ReplacedByToken = rt.Token
        });

        var roles = await userManager.GetRolesAsync(user);
        var (access, expires) = tokens.CreateAccessToken(user, roles);
        await db.SaveChangesAsync(ct);

        return new TokenResponse(access, expires, newToken);
    }
}
