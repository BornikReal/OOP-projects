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

    public IFileSystemEntity CreateEntity(ZipArchiveEntry archiveEntry)
    {
        IEnumerable<IFileSystemEntity> Func()
        {
            var archive = new ZipArchive(archiveEntry.Open(), ZipArchiveMode.Read);
            return ZipObjects.Select(zipObject => zipObject.CreateEntity(archive.Entries.First(x => x.Name == zipObject.Name)));
        }

        return new DirectoryEntity(Name[..^4], Func);
    }
}
