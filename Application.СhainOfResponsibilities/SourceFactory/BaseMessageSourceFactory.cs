using Domain.MessageSource;

namespace Application.СhainOfResponsibilities.SourceFactory;

public abstract class BaseMessageSourceFactory
{
    public abstract BaseMessageSource CreateMessageSource(string label);
}
