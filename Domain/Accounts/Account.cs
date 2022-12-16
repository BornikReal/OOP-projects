using Domain.Messages;
using Domain.MessageSource;
using Domain.Workers;

namespace Domain.Accounts;

public class Account
{
    private readonly HashSet<BaseMessageSource> _sources;
    public Account(AccessLayer access, Guid id)
    {
        Access = access;
        Id = id;
        _sources = new HashSet<BaseMessageSource>();
    }

    public IReadOnlyCollection<BaseMessageSource> Sources => _sources;
    public AccessLayer Access { get; }
    public Guid Id { get; }

    public void AddMessageSource(BaseMessageSource source)
    {
        if (_sources.Add(source) is false)
            throw new ArgumentException("Source already exists", nameof(source));
    }

    public IReadOnlyCollection<BaseMessage> LoadMessage(SlaveWorker worker)
    {
        if (worker.Access.Value < Access.Value)
            throw new UnauthorizedAccessException("Worker has no access to this account");

        var messages = _sources.SelectMany(x => x.Messages).ToList();
        foreach (BaseMessage? message in messages)
            message.LoadMessage();

        return messages;
    }
}
