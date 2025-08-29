using Microsoft.Extensions.FileProviders;
using Tomlyn;

namespace AzLogs.Ingestion;

public class MessageGenerator
{
    private Guid SessionId { get; } = Guid.NewGuid();

    private readonly Dictionary<string, MessageLine> _messageTemplates = new();
    private readonly string _debugMessageTemplateName = "Templates.debug.toml";

    public MessageGenerator(IFileProvider fileProvider)
    {
        // Use fileProvider to load message templates

        // https://stackoverflow.com/questions/62107756/embeddedprovider-getdirectorycontents-returns-0-results
        var directory = fileProvider.GetDirectoryContents("/");
        foreach (var file in directory)
        {
            using var stream = file.CreateReadStream();
            using var reader = new StreamReader(stream);
            var toml = reader.ReadToEnd();
            var template = Toml.ToModel<MessageLine>(toml) ?? throw new Exception($"Unable to parse message template {file.Name}");
            _messageTemplates[file.Name] = template;
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
        var result = _messageTemplates[_debugMessageTemplateName];
        result.TimeOnClient = DateTimeOffset.UtcNow;
        result.Id = Guid.NewGuid().ToString();
        result.Properties.SessionId = SessionId;

        return result;
    }
}