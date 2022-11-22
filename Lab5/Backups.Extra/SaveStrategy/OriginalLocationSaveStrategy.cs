using Backups.Extra.RepositorySuper;
using Backups.Extra.Visitors;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;

namespace Backups.Extra.SaveStrategy;

public class OriginalLocationSaveStrategy : ISaveStrategy
{
    private readonly RestorePointVisitor _restorer = new RestorePointVisitor();
    public void SetNewSaveData(BackupObject backupObject, IFileSystemEntity entity)
    {
        _restorer.SavingPath = $"{backupObject.ObjectPath}";
        if (backupObject.Repository as IRepositorySuper != null)
            _restorer.Repository = (IRepositorySuper)backupObject.Repository;
        else
            throw new Exception();
        entity.Accept(_restorer);
    }

    public override string ToString()
    {
        return "Original location strategy";
    }
}
