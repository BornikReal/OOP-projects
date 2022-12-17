using Domain.Messages;
using Domain.MessageSource;
using Domain.Workers;

namespace Domain.Accounts;

public class Account
{
    public Account(AccessLayer access, Guid id)
    {
        Access = access;
        Id = id;
        SourcesList = new List<BaseMessageSource>();
    }

#pragma warning disable CS8618
    protected Account() { }
    public IReadOnlyCollection<BaseMessageSource> Sources => SourcesList;
    public virtual AccessLayer Access { get; }
    public Guid Id { get; }
    protected virtual List<BaseMessageSource> SourcesList { get; set; }

    public void AddMessageSource(BaseMessageSource source)
    {
        if (!SourcesList.Contains(source))
            throw new ArgumentException("Source already exists", nameof(source));
        SourcesList.Add(source);
    }

    public IReadOnlyCollection<BaseMessage> LoadMessage(SlaveWorker worker)
    {
        if (worker.Access.Value > Access.Value)
            throw new UnauthorizedAccessException("Worker has no access to this account");

        var messages = SourcesList.SelectMany(x => x.Messages).ToList();
        foreach (BaseMessage? message in messages)
            message.LoadMessage();

        return messages;
    }
}
