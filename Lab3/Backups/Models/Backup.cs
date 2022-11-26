using Backups.Exceptions;

namespace Backups.Models;

public class Backup : IBackup
{
    private readonly List<RestorePoint> _restorePoints = new List<RestorePoint>();

    public Backup() { }

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
