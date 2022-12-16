using Domain.Messages;

namespace Application.Dto.Messages;

public record PhoneMessageDto : BaseMessageDto
{
    public PhoneMessageDto(string phoneNumber, string message, Guid id, string label, MessageState state)
        : base(id, message, state, label)
    {
        PhoneNumber = phoneNumber;
    }

    public string PhoneNumber { get; }
}
