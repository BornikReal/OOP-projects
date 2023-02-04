namespace Domain.Common.Exceptions;

public class MessageSourceException : DomainException
{
    private MessageSourceException(string? message)
        : base(message) { }

    public static MessageSourceException MessageAlreadyExistInSource(Guid messageId)
        => new MessageSourceException($"Message {messageId} is already in this source");

    public static MessageSourceException InvalidPhoneNumber(string phoneNumber)
        => new MessageSourceException($"{phoneNumber} isn't a valid phone number");

    public static MessageSourceException InvalidEmailAddress(string emailAddress)
        => new MessageSourceException($"{emailAddress} isn't a valid email address");
}