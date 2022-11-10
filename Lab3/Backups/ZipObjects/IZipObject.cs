using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;

namespace Backups.ZipObjects;

public interface IZipObject
{
    string Name { get; }
    IFileSystemEntity CreateEntity(Func<ZipArchiveEntry> archiveEntry);
}
