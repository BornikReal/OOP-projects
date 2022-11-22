using Backups.Extra.RestorePointVisitors;
using Backups.Extra.Wrappers;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;

namespace Backups.Extra.SaveStrategy;

public class DifferentLocationSaveStrategy : ISaveStrategy
{
    private readonly string _savingPath;
    private readonly IRepositorySuper _repository;

    public DifferentLocationSaveStrategy(string savingPath, IRepositorySuper repository)
    {
        _savingPath = savingPath;
        _repository = repository;
    }

    public void SetNewSaveData(BackupObject backupObject, IFileSystemEntity entity, IRestorePointVisitor restorer)
    {
        restorer.Repository = _repository;
        restorer.SavingPath = $"{_savingPath}{_repository.PathSeparator}{backupObject.ObjectPath[(backupObject.ObjectPath.LastIndexOf(_repository.PathSeparator) + 1) ..]}";
        entity.Accept(restorer);
    }

    public override string ToString()
    {
        return "Different location strategy";
    }
}
