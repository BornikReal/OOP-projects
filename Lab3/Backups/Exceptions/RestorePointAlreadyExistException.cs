namespace Backups.Exceptions;

public class RestorePointAlreadyExistException : BackupException
{
    public RestorePointAlreadyExistException()
        : base("Restore point already exist.")
    { }
}
