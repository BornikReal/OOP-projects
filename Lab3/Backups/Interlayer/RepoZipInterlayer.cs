using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;
using Backups.ZipObjects;

namespace Backups.Interlayer;

public class RepoZipInterlayer : IRepoDisposable
{
    private readonly List<IFileSystemEntity> _entities = new List<IFileSystemEntity>();
    private readonly ZipArchive _archive;

    public RepoZipInterlayer(IEnumerable<IZipObject> zipObjects, ZipArchive archive)
    {
        _archive = archive;
        foreach (IZipObject obj in zipObjects)
        {
            _entities.Add(obj.CreateEntity(archive.Entries.First(x => x.Name == obj.Name)));
        }
    }

    public IEnumerable<IFileSystemEntity> Entities => _entities;

    public void Dispose()
    {
        _archive.Dispose();
        GC.SuppressFinalize(this);
    }
}
