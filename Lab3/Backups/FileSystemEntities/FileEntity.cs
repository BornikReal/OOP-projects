using Backups.FileSystemEntities.Interfaces;
using Backups.Visitors;

namespace Backups.FileSystemEntities;

public class FileEntity : IFileEntity
{
    private readonly Func<Stream> _funcStream;
    public FileEntity(string name, Func<Stream> funcStream)
    {
        Name = name;
        _funcStream = funcStream;
    }

    public string Name { get; }

    public Stream FuncStream() => _funcStream();

    public void Accept(IArchiveVisitor visitor)
    {
        visitor.Visit(this);
    }
}
