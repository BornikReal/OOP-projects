using Domain.Workers;

namespace Application.ChainOfResponsibilities.WorkerFactory;

public abstract class BaseWorkerFactory
{
    public abstract BaseWorker CreateWorker(string name, int access);
}
