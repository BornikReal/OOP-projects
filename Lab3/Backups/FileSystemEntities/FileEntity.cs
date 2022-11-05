using Backups.Visitors;

namespace Backups.FileSystemEntities;

public class FileEntity : IFileEntity
{
    public FileEntity(string name, string fullPath, Func<Stream> funcStream)
    {
        Name = name;
        FullPath = fullPath;
        FuncStream = funcStream;
    }

    public string Name { get; }

    public string FullPath { get; }

    public Func<Stream> FuncStream { get; }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
