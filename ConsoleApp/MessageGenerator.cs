namespace AzLogs.Ingestion;

public static class MessageGenerator
{
    public static ICollection<MessageLine> GenerateMessages()
    {
        // Simulate message generation
        var messages = new List<MessageLine>();
        for (int i = 0; i < 10; i++)
        {
            messages.Add(GenerateMessage());
        }
        return messages;
    }

    private static MessageLine GenerateMessage()
    {
        return new MessageLine
        {
            TimeOnClient = DateTimeOffset.UtcNow,
            Id = Guid.NewGuid(),
            Message = "Persistence detected on host",
            Properties = new MessageProperties
            {
                ReferenceId = Guid.NewGuid(),
                Comment = "This is a sample comment"
            },
            Category = "SampleCategory",
            Severity = SeverityLevel.Debug,
            Campaign = "Technique, no tactic",
            Decoy = new DecoyInfo
            {
                Name = "SampleDecoy",
                Type = DecoyType.NetworkDevice,
                Id = Guid.NewGuid()
            },
            DeviceEventClass = "SampleEventClass",
            SourceHostName = "SampleSourceHost",
            SourceHostId = Guid.NewGuid(),
            DestinationAddress = "SampleDestinationAddress",
            DestinationPort = "8080",
            MitreTechnique = new MitreTechnique
            {
            /*    Tactic = new MitreTactic
                {
                    Name = MitreTacticName.Persistence,
                    Id = "TA0003"
                },*/
                Name = "SSH Authorized Keys",
                Id = "T1059.003"
            },
            FileHash = "abc123hash",
            User = "sampleuser"
        };
    }
}