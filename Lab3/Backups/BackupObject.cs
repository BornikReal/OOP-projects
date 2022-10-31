namespace Backups;

public class BackupObject
{
    public BackupObject(string fileDescriptor)
    {
        FileDescriptor = fileDescriptor;
    }

    public string FileDescriptor { get; }
}
