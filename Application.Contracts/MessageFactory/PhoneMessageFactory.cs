using Domain.Messages;
using Domain.MessageSource;

namespace Application.Contracts.MessageFactory;

public class PhoneMessageFactory : BaseMessageFactory
{
    private readonly string _phoneNumber;

    public PhoneMessageFactory(string phoneNumber, string label, string message)
        : base(label, message)
    {
        _phoneNumber = phoneNumber;
    }

    public override BaseMessage CreateMessage(Func<string, IReadOnlyCollection<BaseMessageSource>> getSources)
    {
        var message = new PhoneMessage(_phoneNumber, _message, Guid.NewGuid(), _label, MessageState.New);
        
        IReadOnlyCollection<BaseMessageSource> sources = getSources(_label);
        foreach (BaseMessageSource source in sources)
        {
            if (source is PhoneMessageSource certSource)
            {
                certSource.AddMessage(message);
                return message;
            }
        }

        throw new Exception();
    }
}
