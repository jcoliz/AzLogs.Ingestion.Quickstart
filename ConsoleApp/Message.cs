namespace AzLogs.Ingestion;

public class Message
{
    public DateTimeOffset TimeOnClient { get; set; }
    public Guid? Id { get; set; } = null;
    public string MessageText { get; set; } = string.Empty;
    public MessageProperties Properties { get; set; }
    public string? Category { get; set; }
    public string? Severity { get; set; }
    public string? Campaign { get; set; }
    public DecoyInfo Decoy { get; set; }
    public string? DeviceEventClass { get; set; }
    public string? SourceAddress { get; set; }
    public string? SourceHostName { get; set; }
    public Guid? SourceHostId { get; set; }
    public string? DestinationAddress { get; set; }
    public string? DestinationPort { get; set; }
    public string? MitreTacticName { get; set; }
    public string? MitreTechniqueName { get; set; }
    public string? FileHash { get; set; }
    public string? User { get; set; }
}

public class DecoyInfo
{
    public string Name { get; set; }
    public string Type { get; set; }
    public Guid Id { get; set; }
}

public class MessageProperties
{
    public Guid? ReferenceId { get; set; }
    public string? Comment { get; set; }
}