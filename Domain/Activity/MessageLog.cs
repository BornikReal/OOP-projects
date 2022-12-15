namespace Domain.Activity;

public record struct MessageLog(Guid sourceId, DateTime stateChangeTime);