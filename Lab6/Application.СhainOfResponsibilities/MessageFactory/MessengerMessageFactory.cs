using Domain.Messages;
using Domain.MessageSource;

namespace Application.ChainOfResponsibilities.MessageFactory;

public class MessengerMessageFactory : BaseMessageFactory
{
    private readonly string _sender;

    public MessengerMessageFactory(string sender)
    {
        _sender = sender;
    }

    public override BaseMessage CreateMessage(string label, string message, Func<string, IReadOnlyCollection<BaseMessageSource>> getSources)
    {
        var messageObj = new MessengerMessage(_sender, message, Guid.NewGuid(), label);

        IReadOnlyCollection<BaseMessageSource> sources = getSources(label);
        foreach (BaseMessageSource source in sources)
        {
            if (source is MessengerMessageSource certSource)
            {
                certSource.AddMessage(messageObj);
                return messageObj;
            }
        }

        throw new Exception();
    }
}
