using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;
using Backups.Visitors;
using Backups.ZipObjects;

namespace Backups.Archiver;

public class ZipArchiver : IArchiver
{
    public IStorage CreateArchive(IEnumerable<IFileSystemEntity> entities, string archivePath, IRepository repository)
    {
        string archiveName = $"ZipStorage-{Guid.NewGuid()}.zip";
        string newArchivePath = $"{archivePath}{repository.PathSeparator}{archiveName}";
        Stream stream = repository.CreateFile(newArchivePath);
        var archive = new ZipArchive(stream, ZipArchiveMode.Create);
        var visitor = new ZipArchiveVisitor(archive);
        foreach (IFileSystemEntity entity in entities)
            entity.Accept(visitor);
        archive.Dispose();
        stream.Close();
        return new ZipStorage(repository, newArchivePath, new ZipDirectory(archiveName, visitor.GetZipObjects()));
    }

    public override string ToString()
    {
        return "Zip Archiver";
    }
}
