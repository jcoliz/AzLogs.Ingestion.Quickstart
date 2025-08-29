using Microsoft.Extensions.FileProviders;
using Tomlyn;

namespace AzLogs.Ingestion;

public class MessageGenerator
{
    private Guid SessionId { get; } = Guid.NewGuid();

    private readonly MessageLine _debugMessageTemplate;

    public MessageGenerator(IFileProvider fileProvider)
    {
        // Use fileProvider to load message templates

        var messageTemplate = fileProvider.GetFileInfo("Templates/MessageTemplate.toml");
        if (messageTemplate.Exists)
        {
            using var stream = messageTemplate.CreateReadStream();
            using var reader = new StreamReader(stream);
            var toml = reader.ReadToEnd();
            _debugMessageTemplate = Toml.ToModel<MessageLine>(toml) ?? throw new Exception("Unable to parse message template");
        }
        else
        {
            throw new FileNotFoundException("Message template not found", "Templates/MessageTemplate.toml");
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
        var result = _debugMessageTemplate;
        result.TimeOnClient = DateTimeOffset.UtcNow;
        result.Id = Guid.NewGuid().ToString();
        result.Properties.SessionId = SessionId;

        return result;
    }
}