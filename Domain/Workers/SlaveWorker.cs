using Domain.Accounts;

namespace Domain.Workers;

public class SlaveWorker : BaseWorker
{
    public SlaveWorker(string name, Guid id, AccessLayer access) : base(name, id, access)
    { }
}
