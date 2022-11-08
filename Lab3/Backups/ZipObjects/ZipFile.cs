using System.IO.Compression;
using Backups.FileSystemEntities;
using Backups.FileSystemEntities.Interfaces;

namespace Backups.ZipObjects;

public class ZipFile : IZipObject
{
    public ZipFile(string name, ZipDirectory? parent)
    {
        Name = name;
        Parent = parent;
    }

    public string Name { get; }

    public ZipDirectory? Parent { get; }

    public IFileSystemEntity CreateEntity(ZipArchiveEntry archiveEntry)
    {
        return new FileEntity(Name, () => archiveEntry.Open());
    }
}
