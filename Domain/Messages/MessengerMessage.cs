namespace Domain.Messages;

public class MessengerMessage : BaseMessage
{
    public MessengerMessage(string sender, string message, Guid id, string label, MessageState state) : base(id, message, state, label)
    {
        Sender = sender;
    }
    
    public string Sender { get; }
}