using System.IO.Compression;
using Backups.FileSystemEntities;
using Backups.FileSystemEntities.Interfaces;

namespace Backups.ZipObjects;

public class ZipDirectory : IZipObject
{
    public ZipDirectory(string name, IEnumerable<IZipObject> zipObjects, ZipDirectory? parent)
    {
        Name = name;
        ZipObjects = zipObjects;
        Parent = parent;
    }

    public string Name { get; }
    public IEnumerable<IZipObject> ZipObjects { get; }
    public ZipDirectory? Parent { get; }

    public IFileSystemEntity CreateEntity(ZipArchiveEntry archiveEntry)
    {
        IEnumerable<IFileSystemEntity> Func()
        {
            var archive = new ZipArchive(archiveEntry.Open(), ZipArchiveMode.Read);
            var entities = new List<IFileSystemEntity>();
            foreach (ZipArchiveEntry entry in archive.Entries)
                entities.Add(ZipObjects.FirstOrDefault(x => x.Name == entry.Name) !.CreateEntity(archiveEntry));
            return entities;
        }

        return new DirectoryEntity(Name, Func);
    }
}
