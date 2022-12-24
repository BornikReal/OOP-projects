using Domain.Messages;
namespace Domain.MessageSource;

public abstract class BaseMessageSource
{
    public BaseMessageSource(Guid id, string label)
    {
        Id = id;
        Label = label;
    }

#pragma warning disable CS8618
    protected BaseMessageSource() { }

    public Guid Id { get; protected init; }
    public string Label { get; protected init; }
    public abstract IReadOnlyCollection<BaseMessage> Messages { get; protected init; }
}
