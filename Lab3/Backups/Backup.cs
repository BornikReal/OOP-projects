namespace Backups;

public class Backup
{
    private List<RestorePoint> _restorePoints;

    public Backup(List<RestorePoint> restorePoints)
    {
        _restorePoints = restorePoints;
    }

    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;
}
