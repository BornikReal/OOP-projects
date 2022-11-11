using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;
using Backups.ZipObjects;

namespace Backups.FileSystemEntities;

public class RepoInterlayer : IDisposable
{
    private readonly ZipArchive _archive;
    public RepoInterlayer(List<IZipObject> zipObjects, ZipArchive archive)
    {
        _archive = archive;
        var enities = new List<IFileSystemEntity>();
        foreach (IZipObject zipObject in zipObjects)
            enities.Add(zipObject.CreateEntity(_archive.Entries.FirstOrDefault(x => x.Name == zipObject.Name) !));
        Entities = enities;
    }

    public IEnumerable<IFileSystemEntity> Entities { get; }

    public void Dispose()
    {
        _archive.Dispose();
        GC.SuppressFinalize(this);
    }
}
