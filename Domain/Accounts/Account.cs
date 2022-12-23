using Domain.Common.Exceptions;
using Domain.Messages;
using Domain.MessageSource;
using Domain.Workers;

namespace Domain.Accounts;

public class Account
{
    private List<BaseMessageSource> _sources = new List<BaseMessageSource>();
    public Account(AccessLayer access, Guid id)
    {
        Access = access;
        Id = id;
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
        if (_sources.Contains(source))
            throw AccountException.SourceAlreadyInAccount(source.Id);
        _sources.Add(source);
    }

    public IReadOnlyCollection<BaseMessage> LoadMessage(SlaveWorker worker)
    {
        if (worker.Access.Value > Access.Value)
            throw AccessLayerException.InsufficientPermissions(Access, worker.Access);

        var messages = _sources.SelectMany(x => x.Messages).ToList();
        foreach (BaseMessage? message in messages)
            message.LoadMessage();

        return messages;
    }
}
