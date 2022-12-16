using Application.Abstractions.DataAccess;
using Domain.Messages;
using Domain.MessageSource;

namespace Application.Contracts.MessageFactory;

public class MessengerMessageFactory : BaseMessageFactory
{
    private readonly string _sender;

    public MessengerMessageFactory(string sender, string label, string message)
        : base(label, message)
    {
        _sender = sender;
    }

    public override BaseMessage CreateMessage(Func<string, IReadOnlyCollection<BaseMessageSource>> getSources)
    {
        var message = new MessengerMessage(_sender, _message, Guid.NewGuid(), _label, MessageState.New);

        IReadOnlyCollection<BaseMessageSource> sources = getSources(_label);
        foreach (BaseMessageSource source in sources)
        {
            if (source is MessengerMessageSource certSource)
            {
                certSource.AddMessage(message);
                return message;
            }
        }

        throw new Exception();
    }
}
