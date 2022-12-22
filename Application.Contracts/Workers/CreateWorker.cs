using Application.СhainOfResponsibilities.WorkerModels;
using MediatR;

namespace Application.Contracts.Workers;

public static class CreateWorker
{
    public record struct Command(Guid sessionId, BaseWorkerModel model, string login, string password) : IRequest<Response>;

    public record struct Response(Guid managerId);
}
