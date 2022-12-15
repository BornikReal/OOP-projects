using Domain.Accounts;

namespace Domain.Workers;

public class MasterWorker : BaseWorker
{
    private readonly HashSet<BaseWorker> _slaves;
    public MasterWorker(string name, Guid id, AccessLayer access) : base(name, id, access)
    {
        _slaves = new HashSet<BaseWorker>();
    }

    public void AddWorker(BaseWorker worker)
    {
        if (_slaves.Add(worker) is false)
            throw new ArgumentException("Worker already exists", nameof(worker));
    }
}
