using Domain.Workers;

namespace Application.СhainOfResponsibilities.WorkerFactory;

public class SlaveWorkerFactory : BaseWorkerFactory
{
    public override BaseWorker CreateWorker(string name, int access)
    {
        return new SlaveWorker(name, Guid.NewGuid(), access);
    }
}
