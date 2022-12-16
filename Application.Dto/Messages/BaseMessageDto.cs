using Domain.Messages;

namespace Application.Dto.Messages;

public abstract record BaseMessageDto(Guid id, string message, MessageState state, string label);