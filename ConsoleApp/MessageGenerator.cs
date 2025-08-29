using Microsoft.Extensions.FileProviders;
using Tomlyn;

namespace AzLogs.Ingestion;

public class MessageGenerator
{
    private Guid SessionId { get; } = Guid.NewGuid();

    private bool HasRunThisSession { get; set; } = false;

    private readonly Dictionary<string, MessageLine> _messageTemplates = new();

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
            template.Properties ??= new MessageProperties();
            template.Properties.Comment = file.Name;
            _messageTemplates[file.Name] = template;
        }
    }

    public ICollection<MessageLine> GenerateMessages()
    {
        var messages = new List<MessageLine>();

        foreach (var template in _messageTemplates.Values)
        {
            var genProps = template.Properties.Generation;
            if (
                (genProps?.Interval == GenerationInterval.Cycle)
                ||
                (genProps?.Interval == GenerationInterval.Session && !HasRunThisSession)
            )
            {
                messages.AddRange(Enumerable.Range(1, genProps.MessagesPerInterval).Select(x => GenerateMessage(template, x)));
            }
        }
        HasRunThisSession = true;
        return messages;
    }

    private MessageLine GenerateMessage(MessageLine template, int SequenceNumber)
    {
        return template with
        {
            TimeOnClient = DateTimeOffset.UtcNow,
            Id = Guid.NewGuid().ToString(),
            Properties = template.Properties with
            {
                SessionId = SessionId,
                SequenceNumber = SequenceNumber
            }
        };
   }
}