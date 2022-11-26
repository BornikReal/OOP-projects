using Backups.Algorithms;
using Backups.Extra.Cleaner;
using Backups.Extra.Deleter;
using Backups.Extra.LoggingEntities;
using Backups.Extra.Models;
using Backups.Extra.RepositorySuper;
using Backups.Strategy;

namespace Backups.Extra.Builders;

public class BackupTaskBuilder : IBackupTaskBuilder
{
    private readonly string _backupPath = $"Backup-{Guid.NewGuid()}";
    private IAlgorithm? _algorithm;
    private IRepositorySuper? _repository;
    private ITimeStrategy? _strategy;
    private ILogger? _logger;
    private ICleaner? _cleaner;
    private IDeleter? _deleter;

    public IBackupTaskBuilder AddMerger()
    {
        if (_repository == null || _logger == null || _algorithm == null)
            throw new NullReferenceException();
        _deleter = new RestorePointMerger(_algorithm, _repository, _backupPath, _logger);
        return this;
    }

    public IBackupTaskBuilder AddNormalDeleter()
    {
        if (_repository == null || _logger == null)
            throw new NullReferenceException();
        _deleter = new RestorePointDeleter(_repository, _logger);
        return this;
    }

    public IBackupTaskBuilder SetAlgorithm(IAlgorithm algorithm)
    {
        _algorithm = algorithm;
        return this;
    }

    public IBackupTaskBuilder SetCleaner(ICleaner cleaner)
    {
        _cleaner = cleaner;
        return this;
    }

    public IBackupTaskBuilder SetLogger(ILogger logger)
    {
        _logger = logger;
        return this;
    }

    public IBackupTaskBuilder SetRepository(IRepositorySuper repository)
    {
        _repository = repository;
        return this;
    }

    public IBackupTaskBuilder SetTimeStrategy(ITimeStrategy strategy)
    {
        _strategy = strategy;
        return this;
    }

    public BackupTaskSuper Build()
    {
        if (_repository == null || _logger == null || _algorithm == null || _strategy == null || _cleaner == null || _deleter == null)
            throw new NullReferenceException();
        return new BackupTaskSuper(_strategy, _repository, _algorithm, _logger, new BackupSuper(_deleter, _cleaner, _backupPath), _backupPath);
    }
}
