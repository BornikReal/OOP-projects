using Application.Dto;
using MediatR;

namespace Application.Contracts.Messages;

public static class LoadMessages
{
    public record struct Command(Guid sessionId) : IRequest<Response>;
    public record struct Response(MessageListDto messages);
}
