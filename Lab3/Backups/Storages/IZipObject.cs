using System.IO.Compression;
using Backups.FileSystemEntities;

namespace Backups.Storages;

public interface IZipObject
{
    IFileSystemEntity Create(ZipArchiveEntry zipArchiveEntry);
}
