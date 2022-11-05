namespace Backups.Models;

public class Backup
{
    private readonly List<RestorePoint> _restorePoints = new List<RestorePoint>();

    public Backup() { }

    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        if (_restorePoints.Find(s => s == restorePoint) != null)
            throw new Exception();
        _restorePoints.Add(restorePoint);
    }
}
