namespace AzLogs.Ingestion.Options;

/// <summary>
/// Options describing where logs should be sent
/// </summary>
public class LogIngestionOptions
{
    /// <summary>
    /// Config file section
    /// </summary>
    public static readonly string Section = "LogIngestion";

    /// <summary>
    /// Data collection endpoint
    /// </summary>
    public Uri? EndpointUri { get; init; }

    /// <summary>
    /// Immutable ID of Data Collection Rule which we want to process the data
    /// </summary>
    public string DcrImmutableId { get; init; } = string.Empty;

    /// <summary>
    /// To which stream will we send the data
    /// </summary>
    public string Stream { get; init; } = string.Empty;
}
