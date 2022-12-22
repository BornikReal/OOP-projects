using Domain.MessageSource;

namespace Application.СhainOfResponsibilities.SourceFactory;

public class PhoneMessageSourceFactory : BaseMessageSourceFactory
{
    public override BaseMessageSource CreateMessageSource(string label)
    {
        return new PhoneMessageSource(Guid.NewGuid(), label);
    }
}
