using Application.Abstractions.DataAccess;
using Application.Dto.Messages;
using Application.Mapping;
using Domain.Accounts;
using Domain.Messages;
using Domain.Workers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Contracts.Messages.ShowMessage;

namespace Application.Messages;

public class ShowMessageHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public ShowMessageHandler(IDatabaseContext context)
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
            throw new InvalidOperationException("Manager can't show messages");

        BaseMessage? message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == request.messageId, cancellationToken);
        if (message == null)
            throw new Exception("Message with this id not found");
        BaseMessageDto dto = message switch
        {
            EmailMessage messageConc => messageConc.AsDto(),
            PhoneMessage messageConc => messageConc.AsDto(),
            MessengerMessage messageConc => messageConc.AsDto(),
            _ => throw new Exception("Unknown message type"),
        };
        return new Response(dto);
    }
}
