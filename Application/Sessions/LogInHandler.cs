using Application.Abstractions.DataAccess;
using Domain.Accounts;
using MediatR;
using static Application.Contracts.Sessions.LogIn;

namespace Application.Sessions;

public class LogInHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public LogInHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        Domain.Accounts.WorkerAuthenticator? workerAuth = _context.WorkerAuthenticators.FirstOrDefault(x => x.login == request.login && x.password == request.password);
        if (workerAuth == null)
            throw new Exception();
        var session = new Session(Guid.NewGuid(), workerAuth.workerId);

        _context.ActiveSessions.Add(session);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(session.Id);
    }
}
