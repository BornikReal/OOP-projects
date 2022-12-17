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

    public Guid Id { get; }
    public string Label { get; }
    public virtual IReadOnlyCollection<BaseMessage> Messages { get; } = null!;
}
