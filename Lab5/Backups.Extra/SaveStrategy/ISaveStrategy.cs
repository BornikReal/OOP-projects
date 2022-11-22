using Backups.FileSystemEntities.Interfaces;
using Backups.Models;

namespace Backups.Extra.SaveStrategy;

public interface ISaveStrategy
{
    void SetNewSaveData(BackupObject backupObject, IFileSystemEntity entity);
}
