using Application.Abstractions.DataAccess;
using Domain.Messages;

namespace Application.MessageFactory;

public class EmailMessageFactory : BaseMessageFactory
{
    private readonly string _sender;
    private readonly string _messageSubject;

    public EmailMessageFactory(string sender, string messageSubject, string label, string message)
        : base(label, message)
    {
        _sender = sender;
        _messageSubject = messageSubject;
    }

    public override BaseMessage CreateMessage(IDatabaseContext databaseContext)
    {
        return new EmailMessage(_sender, _messageSubject, _message, Guid.NewGuid(), _label, MessageState.New);
    }
}
