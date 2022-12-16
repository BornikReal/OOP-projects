using Application.Abstractions.DataAccess;
using Domain.Messages;
using MediatR;
using static Application.Contracts.Messages.CreateMessage;

namespace Application.Messages;

public class CreateMessageHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public CreateMessageHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        BaseMessage message = request.messageFactory.CreateMessage((x) => _context.MessageSources.Where(y => y.Label == x).ToList());

        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(message.Id);
    }
}
