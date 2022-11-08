using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;

namespace Backups.ZipObjects;

public interface IZipObject
{
    string Name { get; }
    ZipDirectory? Parent { get; }
    IFileSystemEntity CreateEntity(ZipArchiveEntry archiveEntry);
}
