using Application.Abstractions.DataAccess;
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
            throw new InvalidOperationException("Session not found.");

        BaseWorker worker = await _context.Workers.FirstAsync(x => x.Id == session.Id, cancellationToken);
        if (worker is not SlaveWorker)
            throw new InvalidOperationException("Manager can't handle messages");

        BaseMessage? message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == request.messageId, cancellationToken);
        if (message == null)
            throw new InvalidOperationException("Message not found.");

        BaseMessageSource source = await _context.MessageSources
            .FirstAsync(x => x.Label == message.Label && x.Messages.Contains(message));
        ((SlaveWorker)worker).HandleMessage(message, source.Id, DateTime.Now);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response();
    }
}
