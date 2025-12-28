namespace NeutralContainer.Data;

public static class RoleNames
{
    public const string Admin = "Admin";
    public const string Moderator = "Moderator";
    public const string Creator = "Creator";
    public const string Commenter = "Commenter";
    public const string AdminAndModerator = Admin + "," + Moderator;

    public static readonly string[] All = { Admin, Moderator, Creator, Commenter };
    public static readonly string[] DefaultRoles = { Creator, Commenter };
}
