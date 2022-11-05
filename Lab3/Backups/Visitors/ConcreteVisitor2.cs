using Backups.FileSystemEntities;
using Backups.Storages;

namespace Backups.Visitors;

public class ConcreteVisitor2 : IVisitor
{
    private readonly Stack<List<IZipObject>> stack = new Stack<List<IZipObject>>();

    public ConcreteVisitor2(List<IZipObject> zipObjects)
    {
        stack.Push(zipObjects);
    }

    public void Visit(IFileEntity fileEnity)
    {
        throw new NotImplementedException();
    }

    public void Visit(IDirectoryEntity directoryEnity)
    {
        throw new NotImplementedException();
    }
}
