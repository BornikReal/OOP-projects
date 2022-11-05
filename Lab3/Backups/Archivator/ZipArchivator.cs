using System.IO.Compression;
using Backups.FileSystemEntities;
using Backups.Visitors;

namespace Backups.Archivator;

public class ZipArchivator : IArchivator
{
    public string Archiveextension { get; } = ".zip";

    public void CreateArchive(List<IFileSystemEntity> entities, Stream archiveStream)
    {
        using var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create);
        var visitor = new CocnreteVisitor(archive);
        foreach (IFileSystemEntity entity in entities)
            entity.Accept(visitor);
    }
}
