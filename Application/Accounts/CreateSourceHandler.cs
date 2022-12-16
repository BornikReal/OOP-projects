using Application.Abstractions.DataAccess;
using Domain.Accounts;
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
        Domain.MessageSource.BaseMessageSource source = request.factory.CreateMessageSource();
        Account? account = _context.Accounts.FirstOrDefault(x => x.Id == request.account);
        if (account == null)
            throw new Exception("Account not found");
        account.AddMessageSource(source);

        _context.MessageSources.Add(source);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response();
    }
}
