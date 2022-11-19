namespace Backups.Exceptions;

public class RepositoryOpenException : BackupException
{
    public RepositoryOpenException()
        : base("This entity not a file or directory.")
    { }
}
