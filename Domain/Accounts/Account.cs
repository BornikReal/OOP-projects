using Domain.MessageSource;

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
            throw new Exception();
    }
}
