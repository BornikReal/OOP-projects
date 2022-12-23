using Application.Abstractions.DataAccess;
using Application.Dto;
using Domain.Accounts;
using Domain.Messages;
using Domain.Workers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Contracts.Messages.LoadMessages;

namespace Application.Messages;

public class LoadMessagesHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public LoadMessagesHandler(IDatabaseContext context)
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
            throw new InvalidOperationException("Manager can't load messages");

        IQueryable<BaseMessage> messages = _context.Accounts
            .Where(x => x.Access >= worker.Access)
            .SelectMany(x => x.Sources)
            .SelectMany(x => x.Messages);
        foreach (BaseMessage? message in messages)
            message.LoadMessage();

        await _context.SaveChangesAsync(cancellationToken);

        return new Response(new MessageListDto(messages.Select(x => x.Id).ToList()));
    }
}
