using Application.Abstractions.DataAccess;
using Application.Exceptions;
using Application.Exceptions.NotFound;
using Application.Exceptions.NotSupported;
using Application.ChainOfResponsibilities.WorkerHandlerChain;
using Domain.Accounts;
using Domain.Workers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Contracts.Workers.CreateWorker;

namespace Application.Workers;

public class CreateWorkerHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public CreateWorkerHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {

        Session? session = await _context.ActiveSessions.FirstOrDefaultAsync(x => x.Id == request.sessionId, cancellationToken);
        if (session == null)
            throw EntityNotFoundException<Session>.Create(request.sessionId);

        BaseWorker master = await _context.Workers.FirstAsync(x => x.Id == session.Id, cancellationToken);
        if (master is not MasterWorker)
            throw NotEnoughPermissionsException.WorkerNotEnoughPermissionsException(master.Name);

        var slaveChain = new SlaveWorkerHandler();
        var masterChain = new MasterWorkerHandler();
        slaveChain.SetNext(masterChain);

        BaseWorker? worker = slaveChain.HandleRequest(request.model);
        if (worker == null)
            throw EntityNotSupportedException<BaseWorker>.Create();

        _context.Workers.Add(worker);
        _context.WorkerAuthenticators.Add(new WorkerAuthenticator(request.login, request.password, worker.Id));
        await _context.SaveChangesAsync(cancellationToken);

        return new Response();
    }
}