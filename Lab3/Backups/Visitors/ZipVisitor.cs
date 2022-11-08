using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;
using Backups.ZipObjects;

namespace Backups.Visitors;

public class ZipVisitor : IVisitor
{
    private readonly Stack<ZipArchive> _zipArchives = new Stack<ZipArchive>();
    private readonly Stack<List<IZipObject>> _zipObjects = new Stack<List<IZipObject>>();

    public ZipVisitor(ZipArchive archive)
    {
        _zipArchives.Push(archive);
        _zipObjects.Push(new List<IZipObject>());
    }

    public void Visit(IFileEntity fileEnity)
    {
        using Stream stream = _zipArchives.Peek().CreateEntry(fileEnity.Name).Open();
        using Stream stream1 = fileEnity.FuncStream();
        stream1.CopyTo(stream);
        _zipObjects.Peek().Add(new ZipObjects.ZipFile(fileEnity.Name));
    }

    public void Visit(IDirectoryEntity directoryEnity)
    {
        using Stream stream = _zipArchives.Peek().CreateEntry(directoryEnity.Name + ".zip").Open();
        _zipArchives.Push(new ZipArchive(stream, ZipArchiveMode.Create));
        foreach (IFileSystemEntity entity in directoryEnity.Entities())
            entity.Accept(this);
        _zipArchives.Pop().Dispose();
        List<IZipObject> objects = _zipObjects.Pop();
        _zipObjects.Push(new List<IZipObject>());
        _zipObjects.Peek().Add(new ZipDirectory(directoryEnity.Name, objects));
    }
}
