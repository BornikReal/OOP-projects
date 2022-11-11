using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;
using Backups.Visitors;
using Backups.ZipObjects;

namespace Backups.Archiver;

public class ZipArchivator : IArchivator
{
    public IStorage CreateArchive(List<IFileSystemEntity> entities, string archivePath, IRepository repository)
    {
        string archiveName = "ZipStorage-" + Guid.NewGuid().ToString() + ".zip";
        using Stream stream = repository.CreateFile(archivePath + archiveName);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Create);
        var visitor = new ZipVisitor(archive);
        foreach (IFileSystemEntity entity in entities)
            entity.Accept(visitor);
        return new ZipStorage(repository, archivePath + archiveName, new ZipDirectory(archiveName, visitor.GetZipObjects()));
    }
}
