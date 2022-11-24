using Backups.Exceptions;

namespace Backups.Extra.Exceptions;

public class InvalidBackupObjectRepositoryException : BackupException
{
    public InvalidBackupObjectRepositoryException()
        : base("This backup object has invalid repository")
    { }
}
