namespace AzLogs.Ingestion.Options;

/// <summary>
/// Options describing the identity of the app
/// </summary>
public class IdentityOptions
{
    /// <summary>
    /// Config file section
    /// </summary>
    public static readonly string Section = "Identity";

    /// <summary>
    /// Directory (tenant) ID
    /// </summary>
    public Guid TenantId { get; init; }

    /// <summary>
    /// Application (client) ID
    /// </summary>
    public Guid AppId { get; init; }

    /// <summary>
    /// Client secret value
    /// </summary>
    public string AppSecret { get; init; } = string.Empty;
}
