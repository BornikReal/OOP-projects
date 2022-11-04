using System.IO.Compression;
using Backups.FileSystemEntities;

namespace Backups.Repository;

public class Archivator
{
    public void CreateArchive(List<IFileSystemEntity> entities, FileSystemRepository repository, string fullArchiveName)
    {
        FileEntity newArchive = repository.OpenFile(fullArchiveName);
        using (var archive = new ZipArchive(newArchive.Stream!, ZipArchiveMode.Create))
        {
            foreach (IFileSystemEntity entity in entities)
                AddEntity(entity, archive);
        }

        repository.CloseEntity(newArchive);
    }

    public void UnpackArchive(Storage storage, FileSystemRepository repository, string fullUnpackDirectory)
    {
        FileEntity archive = repository.OpenFile(storage.StotagePath);

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
            ZipArchiveEntry archiveEntry = archive.CreateEntry(entity.FullPath);
            using Stream stream = archiveEntry.Open();
            entity.Stream!.CopyTo(stream);
            stream.Close();
        }
    }
}
