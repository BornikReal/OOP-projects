using Application.ChainOfResponsibilities.WorkerFactory;
using Application.ChainOfResponsibilities.WorkerModels;
using Domain.Workers;

namespace Application.ChainOfResponsibilities.WorkerHandlerChain;

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