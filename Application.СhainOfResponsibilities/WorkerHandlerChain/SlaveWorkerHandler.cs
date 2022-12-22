using Application.СhainOfResponsibilities.WorkerFactory;
using Application.СhainOfResponsibilities.WorkerModels;
using Domain.Workers;

namespace Application.СhainOfResponsibilities.WorkerHandlerChain;

public class SlaveWorkerHandler : BaseWorkerHandler
{
    public override BaseWorker? HandleRequest(BaseWorkerModel workerModel)
    {
        if (workerModel is SlaveWorkerModel model)
        {
            var factory = new SlaveWorkerFactory();
            return factory.CreateWorker(model.name, model.access);
        }
        else
        {
            return base.HandleRequest(workerModel);
        }
    }
}