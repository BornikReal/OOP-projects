using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;
using Backups.Visitors;
using Backups.ZipObjects;

namespace Backups.Archiver;

public class ZipArchivator : IArchivator
{
    public string ArchiveExtension { get; } = ".zip";

    public ZipStorage CreateArchive(List<IFileSystemEntity> entities, IRepository repository)
    {
        string archiveName = "ZipStorage-" + Guid.NewGuid().ToString() + ArchiveExtension;
        using Stream stream = repository.CreateFile(archiveName);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Create);
        var visitor = new ZipVisitor(archive);
        foreach (IFileSystemEntity entity in entities)
            entity.Accept(visitor);
        return new ZipStorage(repository, archiveName, new ZipDirectory(archiveName, visitor.ZipObjects.Pop()));
    }
}
