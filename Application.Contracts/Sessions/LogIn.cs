using MediatR;

namespace Application.Contracts.Sessions;

public static class LogIn
{
    public record struct Command(string login, string password) : IRequest<Response>;

    public record struct Response(Guid sessionId);
}
