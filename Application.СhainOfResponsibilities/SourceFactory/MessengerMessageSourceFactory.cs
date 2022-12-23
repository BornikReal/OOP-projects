using Domain.MessageSource;

namespace Application.СhainOfResponsibilities.SourceFactory;

public class MessengerMessageSourceFactory : BaseMessageSourceFactory
{
    public override BaseMessageSource CreateMessageSource(string label)
    {
        return new MessengerMessageSource(Guid.NewGuid(), label);
    }
}
