namespace Domain.Messages;

public class MessengerMessage : BaseMessage
{
    public MessengerMessage(string sender, string message, Guid id) : base(id)
    {
        Sender = sender;
        Message = message;
    }

    public string Sender { get; }
    public string Message { get; }
    public override string GetMessage()
    {
        return Message;
    }
}