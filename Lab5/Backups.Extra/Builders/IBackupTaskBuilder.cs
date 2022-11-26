using Backups.Algorithms;
using Backups.Extra.Cleaner;
using Backups.Extra.LoggingEntities;
using Backups.Extra.Models;
using Backups.Extra.RepositorySuper;
using Backups.Strategy;

namespace Backups.Extra.Builders;

public interface IBackupTaskBuilder
{
    IBackupTaskBuilder AddMerger();
    IBackupTaskBuilder AddNormalDeleter();
    IBackupTaskBuilder SetAlgorithm(IAlgorithm algorithm);
    IBackupTaskBuilder SetRepository(IRepositorySuper repository);
    IBackupTaskBuilder SetTimeStrategy(ITimeStrategy strategy);
    IBackupTaskBuilder SetLogger(ILogger logger);
    IBackupTaskBuilder SetCleaner(ICleaner cleaner);
    BackupTaskSuper Build();
}
