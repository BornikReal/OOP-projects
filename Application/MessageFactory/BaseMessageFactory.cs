using Application.Abstractions.DataAccess;
using Domain.Messages;

namespace Application.MessageFactory;

public abstract class BaseMessageFactory
{
    protected readonly string _label;
    protected readonly string _message;
    public BaseMessageFactory(string label, string message)
    {
        _label = label;
        _message = message;
    }
    
    public abstract BaseMessage CreateMessage(IDatabaseContext databaseContext);
}
