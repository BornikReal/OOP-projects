using Domain.Messages;
using Domain.MessageSource;
using Domain.Workers;

namespace Domain.Accounts;

public class Account
{
    private readonly HashSet<BaseMessageSource> _sources;
    public Account(AccessLayer access)
    {
        Access = access;
        _sources = new HashSet<BaseMessageSource>();
    }

    public IReadOnlyCollection<BaseMessageSource> Sources => _sources;
    public AccessLayer Access { get; }

    public void AddMessageSource(BaseMessageSource source)
    {
        if (_sources.Add(source) is false)
            throw new ArgumentException("Source already exists", nameof(source));
    }

    public BaseMessage? LoadMessage(SlaveWorker worker, Guid messageId)
    {
        if (worker.Access.Access < Access.Access)
            throw new UnauthorizedAccessException("Worker has no access to this account");

        foreach (BaseMessageSource source in _sources)
        {
            BaseMessage? message = source.Messages.FirstOrDefault(m => m.Id == messageId);
            if (message is not null)
            {
                message.State = Messages.MessageState.Received;
                return message;
            }
        }

        return null;
    }
}
