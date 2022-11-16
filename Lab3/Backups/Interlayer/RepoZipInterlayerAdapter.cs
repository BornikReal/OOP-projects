using Backups.FileSystemEntities.Interfaces;

namespace Backups.Interlayer;

public class RepoZipInterlayerAdapter : IRepoDisposable
{
    private readonly IEnumerable<IRepoDisposable> _interlayers;

    public RepoZipInterlayerAdapter(IEnumerable<IRepoDisposable> interlayers)
    {
        _interlayers = interlayers;
    }

    public IEnumerable<IFileSystemEntity> Entities => _interlayers.SelectMany(x => x.Entities);

    public void Dispose()
    {
        foreach (IRepoDisposable interlayer in _interlayers)
            interlayer.Dispose();
        GC.SuppressFinalize(this);
    }
}
