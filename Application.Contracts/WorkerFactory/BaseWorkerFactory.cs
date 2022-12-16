using Domain.Workers;

namespace Application.Contracts.WorkerFactory;

public abstract class BaseWorkerFactory
{
    protected readonly string _name;
    protected readonly int _access;

    public BaseWorkerFactory(string name, int access)
    {
        _name = name;
        _access = access;
    }

    public abstract BaseWorker CreateWorker();
}
