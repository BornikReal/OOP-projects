using Application.ChainOfResponsibilities.WorkerFactory;
using Application.ChainOfResponsibilities.WorkerModels;
using Domain.Workers;

namespace Application.ChainOfResponsibilities.WorkerHandlerChain;

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