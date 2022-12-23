using Domain.Messages;
using Domain.MessageSource;

namespace Application.СhainOfResponsibilities.MessageFactory;

public class EmailMessageFactory : BaseMessageFactory
{
    private readonly string _sender;
    private readonly string _messageSubject;

    public EmailMessageFactory(string sender, string messageSubject)
    {
        _sender = sender;
        _messageSubject = messageSubject;
    }

    public override BaseMessage CreateMessage(string label, string message, Func<string, IReadOnlyCollection<BaseMessageSource>> getSources)
    {
        var messageObj = new EmailMessage(_sender, _messageSubject, message, Guid.NewGuid(), label);
        IReadOnlyCollection<BaseMessageSource> sources = getSources(label);
        foreach (BaseMessageSource source in sources)
        {
            if (source is EmailMessageSource certSource)
            {
                certSource.AddMessage(messageObj);
                return messageObj;
            }
        }

        throw new Exception();
    }
}
