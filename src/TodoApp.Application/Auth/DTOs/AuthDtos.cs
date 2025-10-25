
namespace TodoApp.Application.Auth.DTOs;

public record RegisterRequest(string Email, string Password);
public record LoginRequest(string Email, string Password);
public record TokenResponse(string AccessToken, DateTime ExpiresUtc, string RefreshToken);
