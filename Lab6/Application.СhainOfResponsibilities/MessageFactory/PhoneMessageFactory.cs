using Domain.Messages;
using Domain.MessageSource;

namespace Application.ChainOfResponsibilities.MessageFactory;

public class PhoneMessageFactory : BaseMessageFactory
{
    private readonly string _phoneNumber;

    public PhoneMessageFactory(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
    }

    public override BaseMessage CreateMessage(string label, string message, Func<string, IReadOnlyCollection<BaseMessageSource>> getSources)
    {
        var messageObj = new PhoneMessage(_phoneNumber, message, Guid.NewGuid(), label);
        
        IReadOnlyCollection<BaseMessageSource> sources = getSources(label);
        foreach (BaseMessageSource source in sources)
        {
            if (source is PhoneMessageSource certSource)
            {
                certSource.AddMessage(messageObj);
                return messageObj;
            }
        }

        throw new Exception();
    }
}
