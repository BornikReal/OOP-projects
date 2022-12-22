using Domain.Workers;

namespace Application.СhainOfResponsibilities.WorkerFactory;

public abstract class BaseWorkerFactory
{
    public abstract BaseWorker CreateWorker(string name, int access);
}
