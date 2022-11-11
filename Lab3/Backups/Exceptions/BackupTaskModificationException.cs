namespace Backups.Exceptions;

public class BackupTaskModificationException : BackupException
{
    public BackupTaskModificationException()
        : base("BackupObject can't be added or removed from BackupTask.")
    { }
}
