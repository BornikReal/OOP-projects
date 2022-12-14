using Domain.Messages;

namespace Domain.MessageSource;

public class MessageSource<TMessage>
    where TMessage : BaseMessage
{
    public MessageSource(Guid id, ICollection<TMessage> messages)
    {
        Id = id;
        Messages = messages;
    }

    public Guid Id { get; }
    public virtual ICollection<TMessage> Messages { get; }
}
