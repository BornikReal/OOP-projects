using Domain.Messages;

namespace Domain.MessageSource;

public interface IMessageSource
{
    Guid Id { get; }
    IReadOnlyCollection<IBaseMessage> Messages { get; }
}
