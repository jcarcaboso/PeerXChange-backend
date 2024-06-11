namespace UsersManagement.Persistence;

public sealed record UserSettings
{
    public const string SettingsKey = "UserSettings";
    
    public required DeleteSettings Delete { init; get; }
}

public sealed record DeleteSettings
{
    public required int GracePeriodInDays { init; get; }
} 