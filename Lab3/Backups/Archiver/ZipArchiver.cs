using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;
using Backups.Storages;
using Backups.Visitors;
using Backups.ZipObjects;

namespace Backups.Archiver;

public class ZipArchiver : IArchivator
{
    public IStorage CreateArchive(List<IFileSystemEntity> entities, string archivePath, IRepository repository)
    {
        string archiveName = "ZipStorage-" + Guid.NewGuid().ToString() + ".zip";
        Stream stream = repository.CreateFile(archivePath + repository.PathSeparator + archiveName);
        var archive = new ZipArchive(stream, ZipArchiveMode.Create);
        var visitor = new ZipVisitor(archive);
        foreach (IFileSystemEntity entity in entities)
            entity.Accept(visitor);
        archive.Dispose();
        stream.Close();
        return new ZipStorage(repository, archivePath + repository.PathSeparator + archiveName, new ZipDirectory(archiveName, visitor.GetZipObjects()));
    }
}
