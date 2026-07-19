namespace CampCenter.Application.DTOs.Auth;

/// <param name="Login">Unique sign-in identifier.</param>
/// <param name="Password">Password in plain text.</param>
public record LoginRequestDto(string Login, string Password);
