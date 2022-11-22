using Backups.Extra.AlgorithmSuper;
using Backups.Extra.Wrappers;
using Backups.FileSystemEntities.Interfaces;
using Backups.Interlayer;
using Backups.Models;
using Backups.Storages;

namespace Backups.Extra.Visitors;

public class AlgorithmVisitor : IAlgorithmVisitor
{
    private readonly IRepositorySuper _repository;
    private readonly string _restorePointPath;
    private readonly IEnumerable<RestorePoint> _points;
    public AlgorithmVisitor(IEnumerable<RestorePoint> points, IRepositorySuper repositorySuper, string restorePointPath)
    {
        _points = points;
        _repository = repositorySuper;
        _restorePointPath = restorePointPath;
    }

    public RestorePoint? Result { get; private set; }
    public IEnumerable<RestorePoint>? DeletablePoints { get; private set; }

    public void Visit(SplitStorageAlgorithmVisitor algorithm)
    {
        RestorePoint point;
        var points = new List<RestorePoint>(_points);
        var disp = new List<IRepoDisposable>();
        IEnumerable<IFileSystemEntity> enities = new List<IFileSystemEntity>();
        IEnumerable<BackupObject> backupObjects = new List<BackupObject>();
        while (points.Any())
        {
            point = points.MaxBy(t => t.CreationTime) !;
            disp.Add(point.Storage.GetEntities());
            enities = enities.Union(disp.Last().Entities);
            backupObjects = backupObjects.Union(point.BackupObjects);
            points.Remove(point);
        }

        _repository.CreateDirectory(_restorePointPath);
        IStorage newStorage = algorithm.CreateBackup(enities, _restorePointPath, _repository);
        Result = new RestorePoint(backupObjects, newStorage, _restorePointPath, DateTime.Now);
        foreach (IRepoDisposable disposable in disp)
        {
            disposable.Dispose();
        }

        DeletablePoints = _points;
    }

    public void Visit(SingleStorageAlgorithmVisitor algorithm)
    {
        RestorePoint point = _points.MaxBy(t => t.CreationTime) !;
        Result = new RestorePoint(point.BackupObjects, point.Storage, _restorePointPath, DateTime.Now);
        DeletablePoints = _points.Where(x => x != point);
    }
}
