using System.IO.Compression;
using Backups.FileSystemEntities;
using Backups.FileSystemEntities.Interfaces;

namespace Backups.ZipObjects;

public class ZipDirectory : IZipObject
{
    public ZipDirectory(string name, IEnumerable<IZipObject> zipObjects)
    {
        Name = name;
        ZipObjects = zipObjects;
    }

    public string Name { get; }
    public IEnumerable<IZipObject> ZipObjects { get; }

    public IFileSystemEntity CreateEntity(ZipArchiveEntry archiveEntry)
    {
        IEnumerable<IFileSystemEntity> Func()
        {
            var archive = new ZipArchive(archiveEntry.Open(), ZipArchiveMode.Read);
            var entities = new List<IFileSystemEntity>();
            foreach (IZipObject obj in ZipObjects)
            {
                if (obj.GetType() == typeof(ZipDirectory))
                    entities.Add(obj.CreateEntity(archive.Entries.First(x => x.Name == $"{obj.Name}.zip")));
                else
                    entities.Add(obj.CreateEntity(archive.Entries.First(x => x.Name == obj.Name)));
            }

            return entities;
        }

        return new DirectoryEntity(Name, Func);
    }
}
