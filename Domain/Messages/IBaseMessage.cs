namespace Domain.Messages;

public interface IBaseMessage
{
    Guid Id { get; }
    string Label { get; }
    MessageState State { get; }
    string Message { get; }
}
