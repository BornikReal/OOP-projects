using Backups.Extra.Cleaner;

namespace Backups.Extra.Deleter;

public interface ICleanable
{
    void Clean(ICleaner cleaner, IDeleter deleter);
}
