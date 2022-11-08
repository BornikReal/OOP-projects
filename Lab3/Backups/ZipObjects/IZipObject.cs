using System.IO.Compression;
using Backups.FileSystemEntities;

namespace Backups.ZipObjects;

public interface IZipObject
{
    string Name { get; }
    IFileSystemEntity CreateEntity(ZipArchiveEntry archiveEntry);
}
