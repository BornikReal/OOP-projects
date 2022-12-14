using Domain.Messages;

namespace Domain.MessageSource;

public interface IMessageSource
{
    Guid Id { get; }
    string Label { get; }
    IReadOnlyCollection<IBaseMessage> Messages { get; }
}
