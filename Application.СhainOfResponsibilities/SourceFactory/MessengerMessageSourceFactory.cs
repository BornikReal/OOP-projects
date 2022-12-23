using Domain.MessageSource;

namespace Application.ChainOfResponsibilities.SourceFactory;

public class MessengerMessageSourceFactory : BaseMessageSourceFactory
{
    public override BaseMessageSource CreateMessageSource(string label)
    {
        return new MessengerMessageSource(Guid.NewGuid(), label);
    }
}
