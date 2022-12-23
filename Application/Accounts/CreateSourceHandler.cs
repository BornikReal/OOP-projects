using Application.Abstractions.DataAccess;
using Application.Exceptions.NotFound;
using Application.Exceptions.NotSupported;
using Application.СhainOfResponsibilities.SourceHandlerChain;
using Domain.Accounts;
using Domain.MessageSource;
using MediatR;
using static Application.Contracts.Accounts.CreateSource;

namespace Application.Accounts;

public class CreateSourceHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public CreateSourceHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var emailChain = new EmailSourceHandler();
        var phoneChain = new PhoneSourceHandler();
        var messengerChain = new MessengerSourceHandler();
        emailChain.SetNext(phoneChain);
        phoneChain.SetNext(messengerChain);

        BaseMessageSource? source = emailChain.HandleRequest(request.model);
        if (source == null)
            throw EntityNotSupportedException<BaseMessageSource>.Create();

        Account? account = _context.Accounts.FirstOrDefault(x => x.Id == request.accountId);
        if (account == null)
            throw EntityNotFoundException<Account>.Create(request.accountId);
        account.AddMessageSource(source);

        _context.MessageSources.Add(source);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response();
    }
}
