namespace Backups.FileSystemEntities;
public class DirectoryEntity : IFileSystemEntity
{
    public DirectoryEntity(string name, string fullPath, IEnumerable<IFileSystemEntity> entities)
    {
        Name = name;
        FullPath = fullPath;
        Entities = entities;
    }

    public string Name { get; }

    public string FullPath { get; }

    public IEnumerable<IFileSystemEntity>? Entities { get; }

    public Stream? Stream => null;
}
