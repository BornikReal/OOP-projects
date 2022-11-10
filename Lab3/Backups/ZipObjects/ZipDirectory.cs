using System.IO.Compression;
using Backups.FileSystemEntities;
using Backups.FileSystemEntities.Interfaces;

namespace Backups.ZipObjects;

public class ZipDirectory : IZipObject
{
    public ZipDirectory(string name, IEnumerable<IZipObject> zipObjects)
    {
        Name = name;
        ZipObjects = zipObjects;
    }

    public string Name { get; }
    public IEnumerable<IZipObject> ZipObjects { get; }

    public IFileSystemEntity CreateEntity(Func<ZipArchiveEntry> archiveEntry)
    {
        IEnumerable<IFileSystemEntity> Func()
        {
            var entities = new List<IFileSystemEntity>();
            foreach (IZipObject zipObject in ZipObjects)
                entities.Add(zipObject.CreateEntity(() => new ZipArchive(archiveEntry().Open(), ZipArchiveMode.Read).Entries.FirstOrDefault(x => x.Name == zipObject.Name) !));
            return entities;
        }

        return new DirectoryEntity(Name, Func);
    }
}
