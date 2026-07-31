namespace CampCenter.Application.DTOs.Users;

/// A panel account as the user list shows it. No password material, ever.
public record AdminUserDto(
    Guid Id,
    string Login,
    string Role,
    DateTime CreatedAt,
    /// True for the account making the request — the panel greys out the controls
    /// that would lock the caller out of their own session.
    bool IsSelf
);

public record CreateUserRequestDto(string Login, string Password, string Role);

public record SetUserRoleRequestDto(string Role);
