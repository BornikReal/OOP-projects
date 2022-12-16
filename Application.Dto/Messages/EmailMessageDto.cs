using Domain.Messages;

namespace Application.Dto.Messages;

public record EmailMessageDto : BaseMessageDto
{
    public EmailMessageDto(string sender, string messageSubject, string message, Guid id, string label, MessageState state)
        : base(id, message, state, label)
    {
        Sender = sender;
        MessageSubject = messageSubject;
    }

    public string Sender { get; }
    public string MessageSubject { get; }
}