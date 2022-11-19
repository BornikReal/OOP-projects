using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;
using Backups.ZipObjects;

namespace Backups.Interlayer;

public class RepoZipInterlayer : IRepoDisposable
{
    private readonly List<IFileSystemEntity> _entities;
    private readonly ZipArchive _archive;

    public RepoZipInterlayer(IEnumerable<IZipObject> zipObjects, ZipArchive archive)
    {
        _archive = archive;
        _entities = zipObjects.Select(zipObject => zipObject.CreateEntity(archive.Entries.First(x => x.Name == zipObject.Name))).ToList();
    }

    public IEnumerable<IFileSystemEntity> Entities => _entities;

    public void Dispose()
    {
        _archive.Dispose();
        GC.SuppressFinalize(this);
    }
}
