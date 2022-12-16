using Application.Abstractions.DataAccess;
using Domain.Messages;

namespace Application.MessageFactory;

public class PhoneMessageFactory : BaseMessageFactory
{
    private readonly string _phoneNumber;

    public PhoneMessageFactory(string phoneNumber, string label, string message)
        : base(label, message)
    {
        _phoneNumber = phoneNumber;
    }

    public override BaseMessage CreateMessage(IDatabaseContext databaseContext)
    {
        return new PhoneMessage(_phoneNumber, _message, Guid.NewGuid(), _label, MessageState.New);
    }
}
