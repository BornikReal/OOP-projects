namespace Domain.Activity;

public record struct MessageLog(Guid SourceId, DateTime StateChangeTime);