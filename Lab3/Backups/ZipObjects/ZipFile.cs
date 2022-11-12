using System.IO.Compression;
using Backups.FileSystemEntities;
using Backups.FileSystemEntities.Interfaces;

namespace Backups.ZipObjects;

public class ZipFile : IZipObject
{
    public ZipFile(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public IFileSystemEntity CreateEntity(ZipArchiveEntry archiveEntry)
    {
        return new FileEntity(Name, () => archiveEntry.Open());
    }
}
