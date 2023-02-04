namespace Domain.Common.Exceptions;

public class MessageException : DomainException
{
    private MessageException(string? message)
        : base(message) { }

    public static MessageException LoadMessage(Guid messageId)
        => new MessageException($"Can't load {messageId} because it's already recieved or processed");

    public static MessageException ProcessedMessage(Guid messageId)
        => new MessageException($"Can't processed {messageId} because it's already processed or new");

    public static MessageException PhoneMessageSizeExceed()
        => new MessageException($"Phone message size exceed 140 bytes");
}