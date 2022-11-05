using System.IO.Compression;
using Backups.FileSystemEntities;

namespace Backups.Visitors;

public class CocnreteVisitor : IVisitor
{
    private readonly Stack<ZipArchive> _zipArchives = new Stack<ZipArchive>();

    public CocnreteVisitor(ZipArchive archive)
    {
        _zipArchives.Push(archive);
    }

    public void Visit(IFileEntity fileEnity)
    {
        ZipArchive archive = _zipArchives.Peek();
        using Stream stream = archive.CreateEntry(fileEnity.Name).Open();
        fileEnity.Stream!.CopyTo(stream);
    }

    public void Visit(IDirectoryEntity directoryEnity)
    {
        ZipArchive archive = _zipArchives.Peek();
        ZipArchiveEntry entry = archive.CreateEntry(directoryEnity.Name);
        using Stream stream = entry.Open();
        _zipArchives.Push(new ZipArchive(stream));
        foreach (IFileSystemEntity entity in directoryEnity.Entities)
            entity.Accept(this);
        _zipArchives.Pop();
    }
}
