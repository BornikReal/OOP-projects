using Domain.Workers;

namespace Application.ChainOfResponsibilities.WorkerFactory;

public class SlaveWorkerFactory : BaseWorkerFactory
{
    public override BaseWorker CreateWorker(string name, int access)
    {
        return new SlaveWorker(name, Guid.NewGuid(), access);
    }
}
