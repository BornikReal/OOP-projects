using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;
using Backups.ZipObjects;

namespace Backups.Interlayer;

public class RepoInterlayer : IRepoDisposable
{
    private readonly List<IFileSystemEntity> _entities = new List<IFileSystemEntity>();

    public RepoInterlayer(IEnumerable<IZipObject> zipObjects, ZipArchive archive)
    {
        Archive = archive;
        foreach (IZipObject zipObject in zipObjects)
            _entities.Add(zipObject.CreateEntity(archive.Entries.FirstOrDefault(x => x.Name == zipObject.Name) !));
    }

    public IEnumerable<IFileSystemEntity> Entities => _entities;
    public ZipArchive Archive { get; }

    public void Dispose()
    {
        Archive.Dispose();
        GC.SuppressFinalize(this);
    }
}
