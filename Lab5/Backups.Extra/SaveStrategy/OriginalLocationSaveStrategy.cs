using Backups.Extra.RestorePointVisitors;
using Backups.Extra.Wrappers;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;

namespace Backups.Extra.SaveStrategy;

public class OriginalLocationSaveStrategy : ISaveStrategy
{
    public void SetNewSaveData(BackupObject backupObject, IFileSystemEntity entity, IRestorePointVisitor restorer)
    {
        restorer.SavingPath = $"{backupObject.ObjectPath}";
        if (backupObject.Repository as IRepositorySuper != null)
            restorer.Repository = (IRepositorySuper)backupObject.Repository;
        else
            throw new Exception();
        entity.Accept(restorer);
    }

    public override string ToString()
    {
        return "Original location strategy";
    }
}
