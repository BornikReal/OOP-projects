using Backups.Repository;

namespace Backups.Extra.Wrappers;

public interface IRepositorySuper : IRepository
{
    void DeleteEntity(string path);
}
