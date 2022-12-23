using Application.Abstractions.DataAccess;
using Application.Dto.Messages;
using Application.Exceptions;
using Application.Exceptions.NotFound;
using Application.Exceptions.NotSupported;
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
            throw EntityNotFoundException<Session>.Create(request.sessionId);

        BaseWorker worker = await _context.Workers.FirstAsync(x => x.Id == session.Id, cancellationToken);
        if (worker is not SlaveWorker)
            throw NotEnoughPermissionsException.WorkerNotEnoughPermissionsException(worker.Name);

        BaseMessage? message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == request.messageId, cancellationToken);
        if (message == null)
            throw EntityNotFoundException<BaseMessage>.Create(request.messageId);
        BaseMessageDto dto = message switch
        {
            EmailMessage messageConc => messageConc.AsDto(),
            PhoneMessage messageConc => messageConc.AsDto(),
            MessengerMessage messageConc => messageConc.AsDto(),
            _ => throw EntityNotSupportedException<BaseMessageDto>.Create(),
        };
        return new Response(dto);
    }
}
