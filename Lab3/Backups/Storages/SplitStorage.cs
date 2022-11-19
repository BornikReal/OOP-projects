using Backups.Interlayer;

namespace Backups.Storages;

public class SplitStorage : IStorage
{
    public SplitStorage(IEnumerable<IStorage> storages)
    {
        Storages = storages;
    }

    public IEnumerable<IStorage> Storages { get; }

    public IRepoDisposable GetEntities()
    {
        return new RepoZipInterlayerAdapter(Storages.Select(storage => storage.GetEntities()).ToList());
    }
}
