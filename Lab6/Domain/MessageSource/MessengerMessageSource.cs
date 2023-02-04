using Domain.Common.Exceptions;
using Domain.Messages;

namespace Domain.MessageSource;

public class MessengerMessageSource : BaseMessageSource
{
    private List<BaseMessage> _messages = new List<BaseMessage>();
    public MessengerMessageSource(Guid id, string label)
        : base(id, label)
    { }

    protected MessengerMessageSource() { }

    public override IReadOnlyCollection<BaseMessage> Messages
    {
        get => _messages;
        protected init => _messages = value.ToList();
    }

    public void AddMessage(MessengerMessage message)
    {
        if (_messages.Contains(message))
            throw MessageSourceException.MessageAlreadyExistInSource(Id);
        _messages.Add(message);
    }
}
