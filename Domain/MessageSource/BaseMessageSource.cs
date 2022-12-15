using Domain.Messages;

namespace Domain.MessageSource;

public abstract class BaseMessageSource : IMessageSource
{
    public BaseMessageSource(Guid id, string label)
    {
        Id = id;
        Label = label;
    }
    
    public Guid Id { get; }

    public string Label { get; }

    public abstract IReadOnlyCollection<BaseMessage> Messages { get; }
}
