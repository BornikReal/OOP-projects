using Backups.Exceptions;
using Backups.Extra.Cleaner;
using Backups.Extra.Deleter;
using Backups.Models;

namespace Backups.Extra.Models;

public class BackupSuper : IBackupSuper
{
    private readonly List<RestorePoint> _restorePoints;
    private ICleaner _cleaner;
    private IDeleter _deleter;

    public BackupSuper(ICleaner cleaner, IDeleter deleter)
    {
        _restorePoints = new List<RestorePoint>();
        _cleaner = cleaner;
        _deleter = deleter;
    }

    public IEnumerable<RestorePoint> RestorePoints => _restorePoints;
    public ICleaner Cleaner
    {
        set
        {
            _cleaner = value;
        }
    }

    public IDeleter Deleter
    {
        set
        {
            _deleter = value;
        }
    }

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

    public void Clean()
    {
        IEnumerable<RestorePoint> points = _cleaner.Clean(_restorePoints);
        if (points.Count() == _restorePoints.Count)
            throw new Exception();
        _deleter.DeleteRestorePoint(points);
    }
}
