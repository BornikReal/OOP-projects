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
        var repDispos = new List<IRepoDisposable>();
        foreach (IStorage storage in Storages)
            repDispos.Add(storage.GetEntities());
        return new RepoZipInterlayerAdapter(repDispos);
    }
}
