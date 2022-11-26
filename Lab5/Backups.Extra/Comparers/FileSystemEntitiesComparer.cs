using Backups.FileSystemEntities.Interfaces;

namespace Backups.Extra.Comparers;

public class FileSystemEntitiesComparer : IEqualityComparer<IFileSystemEntity>
{
    public bool Equals(IFileSystemEntity? x, IFileSystemEntity? y)
    {
        if (x as IFileEntity != null && y as IFileEntity != null)
            return x.Name == y.Name;
        else if (x as IDirectoryEntity != null && y as IDirectoryEntity != null)
            return x.Name == y.Name;
        else
            return false;
    }

    public int GetHashCode(IFileSystemEntity obj)
    {
        return obj.Name.GetHashCode();
    }
}
