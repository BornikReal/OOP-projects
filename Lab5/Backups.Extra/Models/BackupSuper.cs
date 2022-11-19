using Backups.Exceptions;
using Backups.Models;

namespace Backups.Extra.Models;

public class BackupSuper : IBackupSuper
{
    private readonly List<RestorePoint> _restorePoints;

    public BackupSuper(IEnumerable<RestorePoint>? points = null)
    {
        if (points == null)
            _restorePoints = new List<RestorePoint>();
        else
            _restorePoints = new List<RestorePoint>(points);
    }

    public IEnumerable<RestorePoint> RestorePoints => _restorePoints;

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        if (_restorePoints.Find(s => s == restorePoint) != null)
            throw new RestorePointAlreadyExistException();
        _restorePoints.Add(restorePoint);
    }

    public void RemoveRestorePoint(RestorePoint restorePoint)
    {
        if (_restorePoints.Find(s => s == restorePoint) == null)
            throw new RestorePointAlreadyExistException();
        _restorePoints.Remove(restorePoint);
    }
}
