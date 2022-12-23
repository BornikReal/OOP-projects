using Domain.Workers;

namespace Application.СhainOfResponsibilities.WorkerFactory;

public class MasterWorkerFactory : BaseWorkerFactory
{
    public override BaseWorker CreateWorker(string name, int access)
    {
        return new MasterWorker(name, Guid.NewGuid(), access);
    }
}
