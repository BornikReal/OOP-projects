using Application.Abstractions.DataAccess;
using Domain.Accounts;
using MediatR;
using static Application.Contracts.Accounts.CreateAccount;

namespace Application.Accounts;

public class CreateAccountHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public CreateAccountHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var account = new Account(request.access, Guid.NewGuid());

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(account.Id);
    }
}
