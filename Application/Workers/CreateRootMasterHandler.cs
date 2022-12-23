using Application.Abstractions.DataAccess;
using Domain.Accounts;
using Domain.Workers;
using MediatR;
using static Application.Contracts.Workers.CreateRootMaster;

namespace Application.Workers;

public class CreateRootMasterHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public CreateRootMasterHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        if (_context.Workers.Any())
            throw new InvalidOperationException("Root master already exists");
        
        var master = new MasterWorker(request.name, Guid.NewGuid(), 0);
        _context.Workers.Add(master);
        _context.WorkerAuthenticators.Add(new WorkerAuthenticator(request.login, request.password, master.Id));
        await _context.SaveChangesAsync(cancellationToken);

        return new Response();
    }
}