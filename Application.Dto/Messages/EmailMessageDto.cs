using Domain.Messages;

namespace Application.Dto.Messages;

public record EmailMessageDto(string sender, string messageSubject, string message, Guid id, string label, MessageState state)
        : BaseMessageDto(id, message, state, label);