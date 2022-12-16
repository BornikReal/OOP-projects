using Application.Abstractions.DataAccess;
using Domain.Messages;

namespace Application.MessageFactory;

public class MessengerMessageFactory : BaseMessageFactory
{
    private readonly string _sender;

    public MessengerMessageFactory(string sender, string label, string message)
        : base(label, message)
    {
        _sender = sender;
    }

    public override BaseMessage CreateMessage(IDatabaseContext databaseContext)
    {
        return new MessengerMessage(_sender, _message, Guid.NewGuid(), _label, MessageState.New);
    }
}
