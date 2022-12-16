using Domain.Workers;

namespace Application.Contracts.WorkerFactory;

public class MasterWorkerFactory : BaseWorkerFactory
{
    public MasterWorkerFactory(string name, int access)
        : base(name, access) { }

    public override BaseWorker CreateWorker()
    {
        return new MasterWorker(_name, Guid.NewGuid(), _access);
    }
}
