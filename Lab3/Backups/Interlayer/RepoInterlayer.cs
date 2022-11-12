using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;
using Backups.ZipObjects;

namespace Backups.Interlayer;

public class RepoInterlayer : IRepoDisposable
{
    private readonly List<IFileSystemEntity> _entities = new List<IFileSystemEntity>();
    private readonly ZipArchive _archive;

    public RepoInterlayer(IEnumerable<IZipObject> zipObjects, ZipArchive archive)
    {
        _archive = archive;
        foreach (IZipObject zipObject in zipObjects)
            _entities.Add(zipObject.CreateEntity(archive.Entries.First(x => x.Name == zipObject.Name)));
    }

    public IEnumerable<IFileSystemEntity> Entities => _entities;

    public void Dispose()
    {
        _archive.Dispose();
        GC.SuppressFinalize(this);
    }
}
