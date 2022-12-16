using Domain.Messages;
using Domain.MessageSource;

namespace Application.Contracts.MessageFactory;

public abstract class BaseMessageFactory
{
    protected readonly string _label;
    protected readonly string _message;
    public BaseMessageFactory(string label, string message)
    {
        _label = label;
        _message = message;
    }
    
    public abstract BaseMessage CreateMessage(Func<string, IReadOnlyCollection<BaseMessageSource>> getSources);
}
