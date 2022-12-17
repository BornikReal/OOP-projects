using Domain.Messages;

namespace Domain.MessageSource;

public class MessengerMessageSource : BaseMessageSource
{
    private readonly List<MessengerMessage> _messages;
    public MessengerMessageSource(Guid id, string label)
        : base(id, label)
    {
        _messages = new List<MessengerMessage>();
    }

#pragma warning disable CS8618
    protected MessengerMessageSource() { }

    public override IReadOnlyCollection<BaseMessage> Messages => _messages;

    public void AddMessage(MessengerMessage message)
    {
        if (!_messages.Contains(message))
            throw new InvalidOperationException("Message already exists.");
        _messages.Add(message);
    }
}
