using Application.Abstractions.DataAccess;
using Application.СhainOfResponsibilities.MessageHandlerChain;
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
        var emailChain = new EmailMessageHandler();
        var phoneChain = new PhoneMessageHandler();
        var messengerChain = new MessengerMessageHandler();
        emailChain.SetNext(phoneChain);
        phoneChain.SetNext(messengerChain);

        BaseMessage? message = emailChain.HandleRequest(request.messageModel, x => _context.MessageSources.Where(y => y.Label == x).ToList());
        if (message == null)
            throw new Exception("This message isn't supported");

        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(message.Id);
    }
}
