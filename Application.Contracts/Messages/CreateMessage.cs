using Application.СhainOfResponsibilities.MessegeModels;
using MediatR;

namespace Application.Contracts.Messages;

public class CreateMessage
{
    public record struct Command(BaseMessageModel messageModel) : IRequest<Response>;
    public record struct Response(Guid messageId);
}
