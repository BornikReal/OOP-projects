using Application.ChainOfResponsibilities.WorkerModels;
using Domain.Workers;

namespace Application.ChainOfResponsibilities.WorkerHandlerChain;

public class BaseWorkerHandler
{
    private BaseWorkerHandler? _nextHandler;

    public BaseWorkerHandler SetNext(BaseWorkerHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public virtual BaseWorker? HandleRequest(BaseWorkerModel workerModel)
    {
        return _nextHandler?.HandleRequest(workerModel);
    }
}
