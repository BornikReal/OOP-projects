using System.IO.Compression;
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
        throw new NotImplementedException();
    }
}
