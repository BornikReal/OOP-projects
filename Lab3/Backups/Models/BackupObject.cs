using Backups.Repository;

namespace Backups.Models;

public class BackupObject
{
    public BackupObject(string objectPath, IRepository repository)
    {
        ObjectPath = objectPath;
        Repository = repository;
    }

    public string ObjectPath { get; }

    public IRepository Repository { get; }
}
