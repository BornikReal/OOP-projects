namespace Domain.Messages;

public abstract class BaseMessage : IBaseMessage
{
    public BaseMessage(Guid id, string message, MessageState state, string label)
    {
        Id = id;
        Message = message;
        State = state;
        Label = label;
    }
    
    public Guid Id { get; }
    public string Label { get; }
    public MessageState State { get; set; }
    public string Message { get; }
}
