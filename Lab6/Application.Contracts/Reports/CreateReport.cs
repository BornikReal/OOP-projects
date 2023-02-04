using Application.Dto;
using MediatR;

namespace Application.Contracts.Reports;

public static class CreateReport
{
    public record struct Command(Guid sessionId, DateTime time, TimeSpan duration) : IRequest<Response>;
    public record struct Response(ReportDto report);
}
