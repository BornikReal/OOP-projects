using System.IO.Compression;
using Backups.FileSystemEntities;

namespace Backups.ZipObjects;

public class ZipDirectory : IZipObject
{
    public IFileSystemEntity Create(ZipArchiveEntry zipArchiveEntry)
    {
        throw new NotImplementedException();
    }
}
