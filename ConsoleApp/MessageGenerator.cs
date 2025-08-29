using Microsoft.Extensions.FileProviders;
using Tomlyn;

namespace AzLogs.Ingestion;

public class MessageGenerator
{
    private Guid SessionId { get; } = Guid.NewGuid();

    public MessageGenerator(IFileProvider fileProvider)
    {
        // Use fileProvider to load message templates

        var messageTemplate = fileProvider.GetFileInfo("Templates/MessageTemplate.toml");
        if (messageTemplate.Exists)
        {
            using var stream = messageTemplate.CreateReadStream();
            using var reader = new StreamReader(stream);
            var toml = reader.ReadToEnd();
            var template = Toml.ToModel<MessageLine>(toml) ?? throw new Exception("Unable to parse message template");
        }
    }
    
    public ICollection<MessageLine> GenerateMessages()
    {
        // Simulate message generation
        var messages = new List<MessageLine>();
        for (int i = 0; i < 10; i++)
        {
            messages.Add(GenerateMessage());
        }
        return messages;
    }

    private MessageLine GenerateMessage()
    {
        return new MessageLine
        {
            TimeOnClient = DateTimeOffset.UtcNow,
            Id = Guid.NewGuid().ToString(),
            Message = "Persistence detected on host",
            Properties = new MessageProperties
            {
                SessionId = SessionId,
                Comment = "This is a sample comment"
            },
            Category = "SampleCategory",
            Severity = SeverityLevel.Debug,
            Campaign = "Adding Mitre tactic",
            Decoy = new DecoyInfo
            {
                Name = "SampleDecoy",
                Type = DecoyType.NetworkDevice,
                Id = Guid.NewGuid().ToString()
            },
            DeviceEventClass = "SampleEventClass",
            SourceHostName = "SampleSourceHost",
            SourceHostId = Guid.NewGuid().ToString(),
            DestinationAddress = "SampleDestinationAddress",
            DestinationPort = "8080",
            MitreTechnique = new MitreTechnique
            {
                Tactic = new MitreTactic
                {
                    Name = MitreTacticName.Persistence,
                    Id = "TA0003"
                },
                Name = "SSH Authorized Keys",
                Id = "T1059.003"
            },
            FileHash = "abc123hash",
            User = "sampleuser"
        };
    }
}