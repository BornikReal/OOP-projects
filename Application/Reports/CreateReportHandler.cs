using Application.Abstractions.DataAccess;
using Application.Mapping;
using Domain.Accounts;
using Domain.Workers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Contracts.Reports.CreateReport;

namespace Application.Reports;

public class CreateReportHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public CreateReportHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        Session? session = await _context.ActiveSessions.FirstOrDefaultAsync(x => x.Id == request.sessionId, cancellationToken);
        if (session == null)
            throw new InvalidOperationException("Session not found.");

        BaseWorker worker = await _context.Workers.FirstAsync(x => x.Id == session.Id, cancellationToken);
        if (worker is not MasterWorker)
            throw new InvalidOperationException("Slaves can't create reports");

        Report report = ((MasterWorker)worker).CreateReport(request.time, request.duration);
        _context.Reports.Add(report);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(report.AsDto());
    }
}
