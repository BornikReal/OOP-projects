using Backups.Extra.RestorePointVisitors;

namespace Backups.Extra.SaveStrategy;

public interface ISaveStrategy
{
    void SetNewSaveData(RestorePointVisitor restorer);
}
