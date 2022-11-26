using Backups.Models;

namespace Backups.Extra.Comparers;

public class BackupObjectComparer : IEqualityComparer<BackupObject>
{
    public bool Equals(BackupObject? x, BackupObject? y)
    {
        if (x == null || y == null)
            return false;
        else if (x.ObjectPath == y.ObjectPath && x.Repository == y.Repository)
            return true;
        else
            return false;
    }

    public int GetHashCode(BackupObject obj)
    {
        return obj.ObjectPath.GetHashCode();
    }
}
