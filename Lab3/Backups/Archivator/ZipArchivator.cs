using System.IO.Compression;
using Backups.FileSystemEntities;

namespace Backups.Archivator;

public class ZipArchivator : IArchivator
{
    public string Archiveextension { get; } = ".zip";

    public void CreateArchive(List<IFileSystemEntity> entities, Stream archiveStream)
    {
        using var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create);
        foreach (IFileSystemEntity entity in entities)
            AddEntity(entity, archive);
    }

    private void AddEntity(IFileSystemEntity entity, ZipArchive archive)
    {
        if (entity.Entities != null)
        {
            foreach (IFileSystemEntity entityPart in entity.Entities)
                AddEntity(entityPart, archive);
        }
        else
        {
            ZipArchiveEntry archiveEntry = archive.CreateEntry(entity.Name);
            using Stream stream = archiveEntry.Open();
            entity.Stream!.CopyTo(stream);
            stream.Close();
        }
    }
}
