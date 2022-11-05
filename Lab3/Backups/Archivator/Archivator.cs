using System.IO.Compression;
using Backups.FileSystemEntities;

namespace Backups.Archivator;

public class Archivator : IArchivator
{
    public string Archiveextension { get; } = ".zip";

    public void CreateArchive(List<IFileSystemEntity> entities, Stream archiveStream)
    {
        using var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create);
        foreach (IFileSystemEntity entity in entities)
            AddEntity(entity, archive);
    }

    public void UnpackArchive(Storage storage, Stream archiveStream)
    {
        using var archiveEnity = new ZipArchive(archiveStream, ZipArchiveMode.Read);
        foreach (ZipArchiveEntry entity in archiveEnity.Entries)
        {
            using Stream stream = entity.Open();
            using Stream fileStream = GetStreamFile(storage.Entities, entity.Name) !;
            stream.CopyTo(fileStream);
            stream.Close();
            fileStream.Close();
        }
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

    private Stream? GetStreamFile(IReadOnlyList<IFileSystemEntity> entities, string searchPath)
    {
        Stream? stream = null;
        foreach (IFileSystemEntity entity in entities)
        {
            stream = GetStreamFileCocnrete(entity, searchPath);
            if (stream != null)
                return stream;
        }

        return stream;
    }

    private Stream? GetStreamFileCocnrete(IFileSystemEntity entity, string searchPath)
    {
        if (entity.Entities != null)
        {
            Stream? stream = null;
            foreach (IFileSystemEntity entityPart in entity.Entities)
            {
                stream = GetStreamFileCocnrete(entityPart, searchPath);
                if (stream != null)
                    return stream;
            }

            return stream;
        }
        else
        {
            if (entity.Name == searchPath)
                return entity.Stream;
            return null;
        }
    }
}
