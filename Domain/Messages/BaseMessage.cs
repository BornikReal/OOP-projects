namespace Domain.Messages;

public abstract class BaseMessage
{
    public BaseMessage(Guid id, string message, MessageState state, string label)
    {
        Id = id;
        Message = message;
        State = state;
        Label = label;
    }

#pragma warning disable CS8618
    protected BaseMessage() { }

    public Guid Id { get; protected init; }
    public string Label { get; protected init; }
    public MessageState State { get; protected set; }
    public string Message { get; protected init; }

    public void LoadMessage()
    {
        if (State != MessageState.New)
            throw new InvalidOperationException("Message is not received");
        State = MessageState.Received;
    }

    public void HandleMessage()
    {
        if (State != MessageState.Received)
            throw new InvalidOperationException("Message is not received");
        State = MessageState.Processed;
    }
}
