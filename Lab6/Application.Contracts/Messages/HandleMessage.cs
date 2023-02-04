using MediatR;

namespace Application.Contracts.Messages;

public static class HandleMessage
{
    public record struct Command(Guid sessionId, Guid messageId) : IRequest<Response>;
    public record struct Response();
}
