namespace Backups.Exceptions;

public abstract class BackupException : IOException
{
    public BackupException(string message)
        : base(message)
    { }
}