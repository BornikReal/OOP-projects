using Domain.Messages;

namespace Domain.MessageSource;

public class MessengerMessageSource : IMessageSource
{
    private readonly HashSet<MessengerMessage> _messages;
    public MessengerMessageSource(Guid id, string label)
    {
        Id = id;
        _messages = new HashSet<MessengerMessage>();
        Label = label;
    }

    public Guid Id { get; }
    public IReadOnlyCollection<IBaseMessage> Messages => _messages;
    public string Label { get; }

    public void AddMessage(MessengerMessage message)
    {
        if (_messages.Add(message) is false)
            throw new InvalidOperationException("Message already exists.");
    }
}
