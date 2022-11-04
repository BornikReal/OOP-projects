using System.IO.Compression;
using Backups.FileSystemEntities;

namespace Backups.Repository;

public class Archivator
{
    public Archivator(FileSystemRepository repository)
    {
        Repository = repository;
    }

    public FileSystemRepository Repository { get; }
    public string Archiveextension { get; } = ".zip";

    public void CreateArchive(List<IFileSystemEntity> entities, string fullArchiveName)
    {
        FileEntity newArchive = Repository.OpenFile(fullArchiveName);
        using (var archive = new ZipArchive(newArchive.Stream!, ZipArchiveMode.Create))
        {
            foreach (IFileSystemEntity entity in entities)
                AddEntity(entity, archive);
        }

        Repository.CloseEntity(newArchive);
    }

    public void UnpackArchive(Storage storage, string fullPathUnpackDirectory)
    {
        FileEntity archive = Repository.OpenFile(storage.StotagePath);
        Repository.CreateStorageUnpackDirectory(storage, fullPathUnpackDirectory);
        using (var archiveEnity = new ZipArchive(archive.Stream!, ZipArchiveMode.Read))
        {
            foreach (ZipArchiveEntry entity in archiveEnity.Entries)
            {
                using Stream stream = entity.Open();
                using Stream fileStream = GetStreamFile(storage.Entities, fullPathUnpackDirectory, entity.Name) !;
                stream.CopyTo(fileStream);
                stream.Close();
                fileStream.Close();
            }
        }

        Repository.CloseEntity(archive);
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

    private Stream? GetStreamFile(IReadOnlyList<IFileSystemEntity> entities, string fullPath, string searchPath)
    {
        Stream? stream = null;
        foreach (IFileSystemEntity entity in entities)
        {
            stream = GetStreamFileCocnrete(entity, fullPath, searchPath);
            if (stream != null)
                return stream;
        }

        return stream;
    }

    private Stream? GetStreamFileCocnrete(IFileSystemEntity entity, string fullPath, string searchPath)
    {
        if (entity.Entities != null)
        {
            Stream? stream = null;
            foreach (IFileSystemEntity entityPart in entity.Entities)
            {
                stream = GetStreamFileCocnrete(entityPart, fullPath, searchPath);
                if (stream != null)
                    return stream;
            }

            return stream;
        }
        else
        {
            if (entity.Name == searchPath)
                return Repository.OpenFile(fullPath + Path.DirectorySeparatorChar + searchPath).Stream;
            return null;
        }
    }
}
