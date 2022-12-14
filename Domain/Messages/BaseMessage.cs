namespace Domain.Messages;

public abstract class BaseMessage
{
    public BaseMessage(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; }
    public MessageState State { get; set; } = MessageState.New;
    public abstract string GetMessage();
}
