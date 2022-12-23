using Application.Abstractions.DataAccess;
using Application.Exceptions;
using Application.Exceptions.NotFound;
using Domain.Accounts;
using Domain.Messages;
using Domain.MessageSource;
using Domain.Workers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Contracts.Messages.HandleMessage;

namespace Application.Messages;

public class HandleMessageHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public HandleMessageHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        Session? session = await _context.ActiveSessions.FirstOrDefaultAsync(x => x.Id == request.sessionId, cancellationToken);
        if (session == null)
            throw EntityNotFoundException<Session>.Create(request.sessionId);

        BaseWorker worker = await _context.Workers.FirstAsync(x => x.Id == session.Id, cancellationToken);
        if (worker is not SlaveWorker)
            throw NotEnoughPermissionsException.WorkerNotEnoughPermissionsException(worker.Name);

        BaseMessage? message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == request.messageId, cancellationToken);
        if (message == null)
            throw EntityNotFoundException<BaseMessage>.Create(request.messageId);

        BaseMessageSource source = await _context.MessageSources
            .FirstAsync(x => x.Label == message.Label && x.Messages.Contains(message));
        ((SlaveWorker)worker).HandleMessage(message, Guid.NewGuid(), source.Id, DateTime.Now);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response();
    }
}
