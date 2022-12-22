using Domain.Messages;
using Domain.MessageSource;
using Domain.Workers;

namespace Domain.Accounts;

public class Account
{
    private List<BaseMessageSource> _sources;
    public Account(AccessLayer access, Guid id)
    {
        Access = access;
        Id = id;
        _sources = new List<BaseMessageSource>();
    }

#pragma warning disable CS8618
    protected Account() { }
    public virtual IReadOnlyCollection<BaseMessageSource> Sources
    {
        get => _sources;
        protected init => _sources = value.ToList();
    }

    public virtual AccessLayer Access { get; protected init; }
    public Guid Id { get; protected init; }

    public void AddMessageSource(BaseMessageSource source)
    {
        if (!_sources.Contains(source))
            throw new ArgumentException("Source already exists", nameof(source));
        _sources.Add(source);
    }

    public IReadOnlyCollection<BaseMessage> LoadMessage(SlaveWorker worker)
    {
        if (worker.Access.Value > Access.Value)
            throw new UnauthorizedAccessException("Worker has no access to this account");

        var messages = _sources.SelectMany(x => x.Messages).ToList();
        foreach (BaseMessage? message in messages)
            message.LoadMessage();

        return messages;
    }
}
