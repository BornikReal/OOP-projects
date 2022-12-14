namespace Domain.Messages;

public class EmailMessage : BaseMessage
{
    public EmailMessage(string sender, string messageSubject, string message, Guid id, string label, MessageState state) : base(id, message, state, label)
    {
        Sender = sender;
        MessageSubject = messageSubject;
    }
    
    public string Sender { get; }
    public string MessageSubject { get; }
}
