using Domain.Messages;
using Domain.MessageSource;

namespace Application.СhainOfResponsibilities.MessageFactory;

public abstract class BaseMessageFactory
{
    public abstract BaseMessage CreateMessage(string label, string message, Func<string, IReadOnlyCollection<BaseMessageSource>> getSources);
}
