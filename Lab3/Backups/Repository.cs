namespace Backups;

public class Repository
{
    public Repository(string path)
    {
        ArchiveStoragePath = path;
    }

    public string ArchiveStoragePath { get; }
}
