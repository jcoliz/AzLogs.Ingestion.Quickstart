using System.Text.Json.Serialization;

namespace AzLogs.Ingestion;

public class MessageLine
{
    public DateTimeOffset TimeOnClient { get; set; }
    public Guid? Id { get; set; } = null;
    public string Message { get; set; } = string.Empty;
    public MessageProperties Properties { get; set; } = new();
    public string? Category { get; set; }
    public SeverityLevel? Severity { get; set; }
    public string? Campaign { get; set; }
    public DecoyInfo Decoy { get; set; } = new();
    public string? DeviceEventClass { get; set; }
    public string? SourceAddress { get; set; }
    public string? SourceHostName { get; set; }
    public Guid? SourceHostId { get; set; }
    public string? DestinationAddress { get; set; }
    public string? DestinationPort { get; set; }
    public MitreTechnique MitreTechnique { get; set; } = new();
    public string? FileHash { get; set; }
    public string? User { get; set; }
}

public class MitreTechnique
{
//    public MitreTactic Tactic { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
}

public class MitreTactic
{
    public MitreTacticName Name { get; set; }
    public string Id { get; set; } = string.Empty;
}

public class DecoyInfo
{
    public string Name { get; set; } = string.Empty;
    public DecoyType Type { get; set; }
    public Guid Id { get; set; }
}

public class MessageProperties
{
    public Guid? ReferenceId { get; set; }
    public string? Comment { get; set; }
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