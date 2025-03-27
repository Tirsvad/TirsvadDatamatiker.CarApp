namespace CarApp.Model;

/// <summary>
/// Represents the role of a user.
/// Higher roles have more permissions than lower roles.
/// </summary>
public enum Role
{
    Guest,
    User,
    Admin
}
