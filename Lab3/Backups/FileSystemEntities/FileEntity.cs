using Backups.Visitors;

namespace Backups.FileSystemEntities;

public class FileEntity : IFileEntity
{
    public FileEntity(string name, Func<Stream> funcStream)
    {
        Name = name;
        FuncStream = funcStream;
    }

    public string Name { get; }

    public Func<Stream> FuncStream { get; }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
