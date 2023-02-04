namespace Domain.Activity;

public record MessageLog(Guid id, Guid sourceId, DateTime stateChangeTime);