using Domain.MessageSource;

namespace Application.ChainOfResponsibilities.SourceFactory;

public class EmailMessageSourceFactory : BaseMessageSourceFactory
{
    public override BaseMessageSource CreateMessageSource(string label)
    {
        return new EmailMessageSource(Guid.NewGuid(), label);
    }
}
