using Application.Abstractions.DataAccess;
using Application.Exceptions;
using Application.Exceptions.NotFound;
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
        WorkerAuthenticator? workerAuth = _context.WorkerAuthenticators.FirstOrDefault(x => (x.login.Equals(request.login) && x.password.Equals(request.password)));
        if (workerAuth == null)
            throw EntityNotFoundException<WorkerAuthenticator>.Create($"{request.login} {request.password}");

        if (_context.ActiveSessions.FirstOrDefault(x => x.WorkerId == workerAuth.workerId) != null)
            throw new AlreadyLogInException();
        
        var session = new Session(Guid.NewGuid(), workerAuth.workerId);

        _context.ActiveSessions.Add(session);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(session.Id);
    }
}
