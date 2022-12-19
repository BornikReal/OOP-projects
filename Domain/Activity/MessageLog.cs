namespace Domain.Activity;

public record MessageLog(Guid sourceId, DateTime stateChangeTime);