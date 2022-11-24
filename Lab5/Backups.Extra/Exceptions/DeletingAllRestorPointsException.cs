using Backups.Exceptions;

namespace Backups.Extra.Exceptions;

public class DeletingAllRestorPointsException : BackupException
{
    public DeletingAllRestorPointsException()
        : base("You can't delete all restore points")
    { }
}
