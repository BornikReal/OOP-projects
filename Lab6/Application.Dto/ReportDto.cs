using Domain.Activity;

namespace Application.Dto;

public record ReportDto(Guid id, IReadOnlyCollection<MessageLog> messageLogs);