using Domain.Messages;

namespace Application.Dto.Messages;

public record MessengerMessageDt(string sender, string message, Guid id, string label, MessageState state)
    : BaseMessageDto(id, message, state, label);