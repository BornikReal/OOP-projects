using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;
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

    public IEnumerable<IFileSystemEntity> GetEntities()
    {
        var entities = new List<IFileSystemEntity>();
        var archve = new ZipArchive(Repository.OpenFile(Path).FuncStream());
        foreach (IZipObject zipObject in ZipDirectory.ZipObjects)
        {
            entities.Add(zipObject.CreateEntity(archve.Entries.FirstOrDefault(x => x.Name == zipObject.Name) !));
        }

        return entities;
    }
}
