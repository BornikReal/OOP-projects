using Domain.Messages;

namespace Domain.MessageSource;

public class MessengerMessageSource : BaseMessageSource
{
    public MessengerMessageSource(Guid id, string label)
        : base(id, label)
    {
        MessagesList = new List<MessengerMessage>();
    }

#pragma warning disable CS8618
    protected MessengerMessageSource() { }

    public override IReadOnlyCollection<BaseMessage> Messages
    {
        get => MessagesList;
        protected init => MessagesList = value.Select(x => (MessengerMessage)x).ToList();
    }

    protected virtual List<MessengerMessage> MessagesList { get; set; }

    public void AddMessage(MessengerMessage message)
    {
        if (MessagesList.Contains(message))
            throw new InvalidOperationException("Message already exists.");
        MessagesList.Add(message);
    }
}
