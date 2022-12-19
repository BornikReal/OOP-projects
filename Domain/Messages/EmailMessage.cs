namespace Domain.Messages;

public class EmailMessage : BaseMessage
{
    public EmailMessage(string sender, string messageSubject, string message, Guid id, string label)
        : base(id, message, MessageState.New, label)
    {
        Sender = sender;
        MessageSubject = messageSubject;
    }

#pragma warning disable CS8618
    protected EmailMessage() { }

    public string Sender { get; }
    public string MessageSubject { get; }
}
