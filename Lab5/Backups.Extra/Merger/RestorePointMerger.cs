﻿using Backups.Algorithms;
using Backups.Extra.Deleter;
using Backups.Extra.RepositorySuper;
using Backups.FileSystemEntities.Interfaces;
using Backups.Interlayer;
using Backups.Models;
using Backups.Storages;

namespace Backups.Extra.Merger;

public class RestorePointMerger : IMerger
{
    private readonly IDeleter _deleter;
    public RestorePointMerger(IDeleter deleter)
    {
        _deleter = deleter;
    }

    public RestorePoint Merge(IEnumerable<RestorePoint> points, IAlgorithm algorithm, IRepositorySuper repository, string restorePointPath)
    {
        RestorePoint point;
        var newPoints = points.ToList();
        var disp = new List<IRepoDisposable>();
        var comparer = new FileSystemEntitiesComparer();
        var enities = new List<IFileSystemEntity>().Distinct(comparer).ToList();
        IEnumerable<BackupObject> backupObjects = new List<BackupObject>();
        while (newPoints.Any())
        {
            point = newPoints.MaxBy(t => t.CreationTime) !;
            disp.Add(point.Storage.GetEntities());
            enities = enities.Union(disp.Last().Entities.Distinct(comparer).ToList()).ToList();
            backupObjects = backupObjects.Union(point.BackupObjects);
            newPoints.Remove(point);
        }

        repository.CreateDirectory(restorePointPath);
        IStorage newStorage = algorithm.CreateBackup(enities, restorePointPath, repository);
        point = new RestorePoint(backupObjects, newStorage, restorePointPath, DateTime.Now);
        foreach (IRepoDisposable disposable in disp)
        {
            disposable.Dispose();
        }

        _deleter.DeleteRestorePoint(points);
        return point;
    }
}
