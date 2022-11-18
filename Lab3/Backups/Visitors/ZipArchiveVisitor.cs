using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;
using Backups.ZipObjects;

namespace Backups.Visitors;

public class ZipArchiveVisitor : IArchiveVisitor
{
    private readonly Stack<ZipArchive> _zipArchives = new Stack<ZipArchive>();
    private readonly Stack<List<IZipObject>> _zipObjects = new Stack<List<IZipObject>>();

    public ZipArchiveVisitor(ZipArchive archive)
    {
        _zipArchives.Push(archive);
        _zipObjects.Push(new List<IZipObject>());
    }

    public List<IZipObject> GetZipObjects() => _zipObjects.Pop();

    public void Visit(IFileEntity fileEnity)
    {
        using Stream archiveStream = _zipArchives.Peek().CreateEntry(fileEnity.Name).Open();
        using Stream fileStream = fileEnity.FuncStream();
        fileStream.CopyTo(archiveStream);
        _zipObjects.Peek().Add(new ZipObjects.ZipFile(fileEnity.Name));
    }

    public void Visit(IDirectoryEntity directoryEnity)
    {
        using Stream archiveStream = _zipArchives.Peek().CreateEntry($"{directoryEnity.Name}.zip").Open();
        _zipArchives.Push(new ZipArchive(archiveStream, ZipArchiveMode.Create));

        foreach (IFileSystemEntity entity in directoryEnity.Entities())
            entity.Accept(this);
        _zipArchives.Pop().Dispose();

        List<IZipObject> objects = _zipObjects.Pop();
        _zipObjects.Push(new List<IZipObject>() { new ZipDirectory(directoryEnity.Name, objects) });
    }
}
