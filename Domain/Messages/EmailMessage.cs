namespace Domain.Messages;

public class EmailMessage : BaseMessage
{
    public EmailMessage(string sender, string messageSubject, string message, Guid id) : base(id)
    {
        Sender = sender;
        MessageSubject = messageSubject;
        Message = message;
    }
    
    public string Sender { get; }
    public string MessageSubject { get; }
    public string Message { get; }
    public override string GetMessage()
    {
        return $"{MessageSubject}:{Message}";
    }
}
