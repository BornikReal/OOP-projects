using Domain.MessageSource;

namespace Application.ChainOfResponsibilities.SourceFactory;

public abstract class BaseMessageSourceFactory
{
    public abstract BaseMessageSource CreateMessageSource(string label);
}
