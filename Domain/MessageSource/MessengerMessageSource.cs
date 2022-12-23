using Domain.Messages;

namespace Domain.MessageSource;

public class MessengerMessageSource : BaseMessageSource
{
    private List<BaseMessage> _messages;
    public MessengerMessageSource(Guid id, string label)
        : base(id, label)
    {
        _messages = new List<BaseMessage>();
    }

#pragma warning disable CS8618
    protected MessengerMessageSource() { }

    public override IReadOnlyCollection<BaseMessage> Messages
    {
        get => _messages;
        protected init => _messages = value.ToList();
    }

    public void AddMessage(MessengerMessage message)
    {
        if (_messages.Contains(message))
            throw new InvalidOperationException("Message already exists.");
        _messages.Add(message);
    }
}
