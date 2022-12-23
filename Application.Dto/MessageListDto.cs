namespace Application.Dto;

public record MessageListDto(IReadOnlyCollection<Guid> messageIds);