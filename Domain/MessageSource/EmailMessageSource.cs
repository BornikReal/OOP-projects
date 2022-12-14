using Domain.Messages;

namespace Domain.MessageSource;

public class EmailMessageSource : IMessageSource
{
    private readonly HashSet<EmailMessage> _messages;
    public EmailMessageSource(Guid id)
    {
        Id = id;
        _messages = new HashSet<EmailMessage>();
    }

    public Guid Id { get; }
    public IReadOnlyCollection<IBaseMessage> Messages => _messages;

    public void AddMessage(EmailMessage message)
    {
        if (_messages.Add(message) is false)
            throw new InvalidOperationException("Message already exists.");
    }
}
