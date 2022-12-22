using Domain.MessageSource;

namespace Application.СhainOfResponsibilities.SourceFactory;

public class EmailMessageSourceFactory : BaseMessageSourceFactory
{
    public override BaseMessageSource CreateMessageSource(string label)
    {
        return new EmailMessageSource(Guid.NewGuid(), label);
    }
}
