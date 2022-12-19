namespace Domain.Messages;

public class MessengerMessage : BaseMessage
{
    public MessengerMessage(string sender, string message, Guid id, string label)
        : base(id, message, MessageState.New, label)
    {
        Sender = sender;
    }

#pragma warning disable CS8618
    protected MessengerMessage() { }

    public string Sender { get; }
}