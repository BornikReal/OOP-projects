using Application.СhainOfResponsibilities.MessageModels;
using MediatR;

namespace Application.Contracts.Messages;

public static class CreateMessage
{
    public record struct Command(BaseMessageModel messageModel) : IRequest<Response>;
    public record struct Response(Guid messageId);
}
