using Domain.Messages;

namespace Application.Dto.Messages;

public record PhoneMessageDto(string phoneNumber, string message, Guid id, string label, MessageState state)
    : BaseMessageDto(id, message, state, label);