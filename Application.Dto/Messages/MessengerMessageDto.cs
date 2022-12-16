using Domain.Messages;

namespace Application.Dto.Messages;

public record MessengerMessageDto : BaseMessageDto
{
    public MessengerMessageDto(string sender, string message, Guid id, string label, MessageState state)
        : base(id, message, state, label)
    {
        Sender = sender;
    }

    public string Sender { get; }
}
