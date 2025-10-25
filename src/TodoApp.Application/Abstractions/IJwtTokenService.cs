
using TodoApp.Domain.Users;

namespace TodoApp.Application.Abstractions;

public interface IJwtTokenService
{
    (string accessToken, DateTime expiresUtc) CreateAccessToken(AppUser user, IEnumerable<string> roles);
    string CreateRefreshToken();
}
