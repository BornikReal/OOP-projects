namespace Backups.Storages;

public class SplitStorage
{
    public SplitStorage(IEnumerable<ZipStorage> storages)
    {
        Storages = storages;
    }

    public IEnumerable<ZipStorage> Storages { get; }
}
