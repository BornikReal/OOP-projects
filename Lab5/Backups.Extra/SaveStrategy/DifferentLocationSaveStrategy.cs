using Backups.Extra.Visitors;
using Backups.Extra.Wrappers;
using Backups.FileSystemEntities.Interfaces;
using Backups.Models;

namespace Backups.Extra.SaveStrategy;

public class DifferentLocationSaveStrategy : ISaveStrategy
{
    private readonly string _savingPath;
    private readonly IRepositorySuper _repository;
    private readonly RestorePointVisitor _restorer;

    public DifferentLocationSaveStrategy(string savingPath, IRepositorySuper repository)
    {
        _savingPath = savingPath;
        _repository = repository;
        _restorer = new RestorePointVisitor();
    }

    public void SetNewSaveData(BackupObject backupObject, IFileSystemEntity entity)
    {
        _restorer.Repository = _repository;
        _restorer.SavingPath = $"{_savingPath}{_repository.PathSeparator}{backupObject.ObjectPath[(backupObject.ObjectPath.LastIndexOf(_repository.PathSeparator) + 1) ..]}";
        entity.Accept(_restorer);
    }

    public override string ToString()
    {
        return "Different location strategy";
    }
}
