using Application.СhainOfResponsibilities.WorkerFactory;
using Application.СhainOfResponsibilities.WorkerModels;
using Domain.Workers;

namespace Application.СhainOfResponsibilities.WorkerHandlerChain;

public class MasterWorkerHandler : BaseWorkerHandler
{
    public override BaseWorker? HandleRequest(BaseWorkerModel workerModel)
    {
        if (workerModel is MasterWorkerModel model)
        {
            var factory = new MasterWorkerFactory();
            return factory.CreateWorker(model.name, model.access);
        }
        else
        {
            return base.HandleRequest(workerModel);
        }
    }
}