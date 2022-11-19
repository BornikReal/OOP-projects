using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;
using Backups.Interlayer;
using Backups.Repository;
using Backups.ZipObjects;

namespace Backups.Storages;

public class ZipStorage : IStorage
{
    public ZipStorage(IRepository repository, string path, ZipDirectory zipDirectory)
    {
        Repository = repository;
        Path = path;
        ZipDirectory = zipDirectory;
    }

    public IRepository Repository { get; }
    public string Path { get; }
    public ZipDirectory ZipDirectory { get; }

    public IRepoDisposable GetEntities()
    {
        IFileSystemEntity entity = Repository.OpenEntity(Path);
        if (entity as IFileEntity is null)
            throw new ArgumentException("Path is not a file");
        else
            return new RepoZipInterlayer(ZipDirectory.ZipObjects, new ZipArchive(((IFileEntity)entity).FuncStream()));
    }
}
