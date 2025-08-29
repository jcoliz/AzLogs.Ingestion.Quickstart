using System.Text.Json.Serialization;

namespace AzLogs.Ingestion;

public record MessageLine
{
    public DateTimeOffset TimeOnClient { get; set; }
    public string? Id { get; set; } = null; // GUID changed to string to match template format
    public string Message { get; set; } = string.Empty;
    public MessageProperties Properties { get; set; } = new();
    public string? Category { get; set; }
    public SeverityLevel? Severity { get; set; }
    public string? Campaign { get; set; }
    public DecoyInfo Decoy { get; set; } = new();
    public string? DeviceEventClass { get; set; }
    public string? SourceAddress { get; set; }
    public string? SourceHostName { get; set; }
    public string? SourceHostId { get; set; } // GUID changed to string to match template format
    public string? DestinationAddress { get; set; }
    public string? DestinationPort { get; set; }
    public MitreTechnique MitreTechnique { get; set; } = new();
    public string? FileHash { get; set; }
    public string? User { get; set; }
}

public record MitreTechnique
{
    public MitreTactic Tactic { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
}

public record MitreTactic
{
    public MitreTacticName Name { get; set; }
    public string Id { get; set; } = string.Empty;
}

public record DecoyInfo
{
    public string Name { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public DecoyType Type { get; set; }
    public string? Id { get; set; } = null; // GUID changed to string to match template format
}

public record MessageProperties
{
    public Guid? SessionId { get; set; }
    public string? Comment { get; set; }
    public int SequenceNumber { get; set; }
    public MessageGenerationProperties? Generation { get; set; }
}

public record MessageGenerationProperties
{
    public int MessagesPerInterval { get; set; } = 10;
    public GenerationInterval Interval { get; set; } = GenerationInterval.Cycle;
}

[JsonConverter(typeof(JsonStringEnumConverter<SeverityLevel>))]
public enum SeverityLevel
{
    Unknown,
    Debug,
    Low,
    Medium,
    High,
    Critical
}

[JsonConverter(typeof(JsonStringEnumConverter<DecoyType>))]
public enum DecoyType
{
    VirtualMachine,
    NetworkDevice,
    Application,
    Database,
    File,
    FileShare,
    Identity,
    IoTSensor,
    Other
}

[JsonConverter(typeof(JsonStringEnumConverter<MitreTacticName>))]
public enum MitreTacticName
{
    Unknown,
    Reconnaissance,
    ResourceDevelopment,
    InitialAccess,
    Execution,
    Persistence,
    PrivilegeEscalation,
    DefenseEvasion,
    CredentialAccess,
    Discovery,
    LateralMovement,
    Collection,
    Exfiltration,
    CommandAndControl,
    Impact
}

[JsonConverter(typeof(JsonStringEnumConverter<GenerationInterval>))]
public enum GenerationInterval
{
    Never,
    Session,
    Cycle,
}
