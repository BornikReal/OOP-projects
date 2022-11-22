using Backups.Extra.Wrappers;
using Backups.Visitors;

namespace Backups.Extra.RestorePointVisitors;

public interface IRestorePointVisitor : IArchiveVisitor
{
    public string SavingPath { get; set; }
    public IRepositorySuper? Repository { get; set; }
}
