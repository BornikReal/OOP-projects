using Domain.MessageSource;

namespace Application.Contracts.SourceFactory;

public abstract class BaseMessageSourceFactory
{
    protected readonly string _label;

    public BaseMessageSourceFactory(string label)
    {
        _label = label;
    }

    public abstract BaseMessageSource CreateMessageSource();
}
