using Domain.Messages;

namespace Domain.MessageSource;

public class PhoneMessageSource : IMessageSource
{
    private readonly HashSet<PhoneMessage> _messages;
    public PhoneMessageSource(Guid id)
    {
        Id = id;
        _messages = new HashSet<PhoneMessage>();
    }

    public Guid Id { get; }
    public IReadOnlyCollection<IBaseMessage> Messages => _messages;

    public void AddMessage(PhoneMessage message)
    {
        if (_messages.Add(message) is false)
            throw new InvalidOperationException("Message already exists.");
    }
}
