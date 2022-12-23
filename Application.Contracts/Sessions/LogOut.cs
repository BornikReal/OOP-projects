using MediatR;

namespace Application.Contracts.Sessions;

public static class LogOut
{
    public record struct Command(Guid sessionId) : IRequest<Response>;

    public record struct Response();
}
