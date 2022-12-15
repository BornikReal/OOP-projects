using Domain.Messages;

namespace Domain.MessageSource;

public class MessengerMessageSource : BaseMessageSource
{
    private readonly HashSet<MessengerMessage> _messages;
    public MessengerMessageSource(Guid id, string label) : base(id, label)
    {
        _messages = new HashSet<MessengerMessage>();
    }
    
    public override IReadOnlyCollection<BaseMessage> Messages => _messages;

    public void AddMessage(MessengerMessage message)
    {
        if (_messages.Add(message) is false)
            throw new InvalidOperationException("Message already exists.");
    }
}
