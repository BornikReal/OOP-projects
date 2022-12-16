using MediatR;

namespace Application.Contracts.Messages;

public class CreateMessage
{
    public record struct Command(string login, string password) : IRequest<Response>;
    public record struct Response(Guid sessionId);
}
