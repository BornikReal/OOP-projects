using Domain.Workers;

namespace Application.Contracts.WorkerFactory;

public class SlaveWorkerFactory : BaseWorkerFactory
{
    public SlaveWorkerFactory(string name, int access)
        : base(name, access) { }


    public override BaseWorker CreateWorker()
    {
        return new SlaveWorker(_name, Guid.NewGuid(), _access);
    }
}
