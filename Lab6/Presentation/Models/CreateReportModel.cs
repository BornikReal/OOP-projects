namespace Presentation.Models;

public record CreateReportModel(Guid sessionId, DateTime time, TimeSpan duration);