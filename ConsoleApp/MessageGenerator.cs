namespace AzLogs.Ingestion;

public static class MessageGenerator
{
    public static ICollection<Message> GenerateMessages()
    {
        // Simulate message generation
        var messages = new List<Message>();
        for (int i = 0; i < 10; i++)
        {
            messages.Add(GenerateMessage());
        }
        return messages;
    }

    private static Message GenerateMessage()
    {
        return new Message
        {
            TimeOnClient = DateTimeOffset.UtcNow,
            Id = Guid.NewGuid(),
            MessageText = "Sample message text",
            Properties = new MessageProperties
            {
                ReferenceId = Guid.NewGuid(),
                Comment = "This is a sample comment"
            },
            Category = "SampleCategory",
            Severity = SeverityLevel.Debug,
            Campaign = "SampleCampaign",
            Decoy = new DecoyInfo
            {
                Name = "SampleDecoy",
                Type = "VirtualMachine",
                Id = Guid.NewGuid()
            },
            DeviceEventClass = "SampleEventClass",
            SourceHostName = "SampleSourceHost",
            SourceHostId = Guid.NewGuid(),
            DestinationAddress = "SampleDestinationAddress",
            DestinationPort = "8080",
            MitreTacticName = "Initial Access",
            MitreTechniqueName = "Phishing",
            FileHash = "abc123hash",
            User = "sampleuser"
        };
    }
}