using System.Text.Json.Serialization;

namespace AzLogs.Ingestion.Generators;

/// <summary>
/// ABAP Audit Log data from SAP systems.
/// Schema: https://learn.microsoft.com/en-us/azure/azure-monitor/reference/tables/abapauditlog
/// </summary>
public record ABAPAuditLog
{
    /// <summary>
    /// The name of the table
    /// </summary>
    [JsonPropertyName("_BilledSize")]
    public double? BilledSize { get; init; }

    /// <summary>
    /// A unique identifier for the data record
    /// </summary>
    [JsonPropertyName("_IsBillable")]
    public bool? IsBillable { get; init; }

    /// <summary>
    /// The name of the resource
    /// </summary>
    [JsonPropertyName("_ResourceId")]
    public string? ResourceId { get; init; }

    /// <summary>
    /// A unique identifier for the subscription
    /// </summary>
    [JsonPropertyName("_SubscriptionId")]
    public string? SubscriptionId { get; init; }

    /// <summary>
    /// ABAP Instance Number
    /// </summary>
    [JsonPropertyName("ABAPInstanceNumber")]
    public string? ABAPInstanceNumber { get; init; }

    /// <summary>
    /// ABAP Program Name
    /// </summary>
    [JsonPropertyName("ABAPProgramName")]
    public string? ABAPProgramName { get; init; }

    /// <summary>
    /// Audit Log Class ID
    /// </summary>
    [JsonPropertyName("AuditLogClassId")]
    public string? AuditLogClassId { get; init; }

    /// <summary>
    /// Audit Log Operation
    /// </summary>
    [JsonPropertyName("AuditLogOperation")]
    public string? AuditLogOperation { get; init; }

    /// <summary>
    /// Category of the audit log
    /// </summary>
    [JsonPropertyName("Category")]
    public string? Category { get; init; }

    /// <summary>
    /// SAP Client ID
    /// </summary>
    [JsonPropertyName("ClientId")]
    public string? ClientId { get; init; }

    /// <summary>
    /// Correlation ID
    /// </summary>
    [JsonPropertyName("CorrelationId")]
    public string? CorrelationId { get; init; }

    /// <summary>
    /// Email address
    /// </summary>
    [JsonPropertyName("Email")]
    public string? Email { get; init; }

    /// <summary>
    /// Identity object
    /// </summary>
    [JsonPropertyName("Identity")]
    public string? Identity { get; init; }

    /// <summary>
    /// Level of the log event
    /// </summary>
    [JsonPropertyName("Level")]
    public string? Level { get; init; }

    /// <summary>
    /// Location information
    /// </summary>
    [JsonPropertyName("Location")]
    public string? Location { get; init; }

    /// <summary>
    /// Message ID
    /// </summary>
    [JsonPropertyName("MessageId")]
    public string? MessageId { get; init; }

    /// <summary>
    /// Message text
    /// </summary>
    [JsonPropertyName("MessageText")]
    public string? MessageText { get; init; }

    /// <summary>
    /// Operation ID
    /// </summary>
    [JsonPropertyName("OperationId")]
    public string? OperationId { get; init; }

    /// <summary>
    /// Operation name
    /// </summary>
    [JsonPropertyName("OperationName")]
    public string? OperationName { get; init; }

    /// <summary>
    /// Properties in JSON format
    /// </summary>
    [JsonPropertyName("Properties")]
    public string? Properties { get; init; }

    /// <summary>
    /// SAP System ID
    /// </summary>
    [JsonPropertyName("SAPSystemId")]
    public string? SAPSystemId { get; init; }

    /// <summary>
    /// Source system
    /// </summary>
    [JsonPropertyName("SourceSystem")]
    public string? SourceSystem { get; init; }

    /// <summary>
    /// Terminal IP Address
    /// </summary>
    [JsonPropertyName("TerminalIPAddress")]
    public string? TerminalIPAddress { get; init; }

    /// <summary>
    /// The timestamp (UTC) of when the log was generated
    /// </summary>
    [JsonPropertyName("TimeGenerated")]
    public DateTime? TimeGenerated { get; init; }

    /// <summary>
    /// Transaction Code
    /// </summary>
    [JsonPropertyName("TransactionCode")]
    public string? TransactionCode { get; init; }

    /// <summary>
    /// Type of the record
    /// </summary>
    [JsonPropertyName("Type")]
    public string? Type { get; init; }

    /// <summary>
    /// User identity
    /// </summary>
    [JsonPropertyName("User")]
    public string? User { get; init; }

    /// <summary>
    /// Creates a sample ABAPAuditLog record for testing
    /// </summary>
    public static ABAPAuditLog CreateSample(int i)
    {
        return new ABAPAuditLog
        {
            TimeGenerated = DateTime.UtcNow,
            SAPSystemId = "PRD",
            ClientId = "100",
            User = "DEVELOPER01",
            ABAPInstanceNumber = $"{i:00}",
            ABAPProgramName = $"RSUS{i:000}",
            TransactionCode = $"SE{i:00}",
            AuditLogClassId = $"S_RFC{i:00}",
            AuditLogOperation = $"EXECUTE{i:00}",
            TerminalIPAddress = $"192.168.1.{i:00}",
            MessageId = $"AU{i:00}",
            MessageText = "RFC call executed successfully",
            Level = "Informational",
            Category = "Audit",
            OperationName = "RFCExecution",
            Type = "ABAPAuditLog",
            SourceSystem = "SAP"
        };
    }
}
