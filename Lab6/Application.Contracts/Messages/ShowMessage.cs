using Application.Dto.Messages;
using MediatR;

namespace Application.Contracts.Messages;

public static class ShowMessage
{
    public record struct Command(Guid sessionId, Guid messageId) : IRequest<Response>;
    public record struct Response(BaseMessageDto message);
}
