using Application.Contracts.MessageFactory;
using MediatR;

namespace Application.Contracts.Messages;

public class CreateMessage
{
    public record struct Command(BaseMessageFactory messageFactory) : IRequest<Response>;
    public record struct Response(Guid messageId);
}
