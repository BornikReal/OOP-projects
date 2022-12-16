using Domain.Messages;
using Domain.MessageSource;

namespace Application.Contracts.MessageFactory;

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

    public override BaseMessage CreateMessage(Func<string, IReadOnlyCollection<BaseMessageSource>> getSources)
    {
        var message = new EmailMessage(_sender, _messageSubject, _message, Guid.NewGuid(), _label, MessageState.New);
        IReadOnlyCollection<BaseMessageSource> sources = getSources(_label);
        foreach (BaseMessageSource source in sources)
        {
            if (source is EmailMessageSource certSource)
            {
                certSource.AddMessage(message);
                return message;
            }
        }

        throw new Exception();
    }
}
