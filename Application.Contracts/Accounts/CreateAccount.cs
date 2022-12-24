using MediatR;

namespace Application.Contracts.Accounts;

public static class CreateAccount
{
    public record struct Command(int access) : IRequest<Response>;
    public record struct Response(Guid accountId);
}
