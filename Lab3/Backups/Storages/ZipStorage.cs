using System.IO.Compression;
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
        return new RepoInterlayer(ZipDirectory.ZipObjects, new ZipArchive(Repository.OpenFile(Path).FuncStream()));
    }
}
