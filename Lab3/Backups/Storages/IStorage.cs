using Backups.Interlayer;

namespace Backups.Storages;

public interface IStorage
{
    IRepoDisposable GetEntities();
}
