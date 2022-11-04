namespace Backups.Repository;

public class FileSystemRepository : IRepository
{
    private readonly string _rootPath;
    public FileSystemRepository(string rootPath)
    {
        _rootPath = rootPath;
    }

    public string GetFullPath(string fileSystemPath) => _rootPath + Path.DirectorySeparatorChar + fileSystemPath;
    public bool IsDirectory(string path) => Directory.Exists(GetFullPath(path));
    public bool IsFile(string path) => File.Exists(GetFullPath(path));
}
