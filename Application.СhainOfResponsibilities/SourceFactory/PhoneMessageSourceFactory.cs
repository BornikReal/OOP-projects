using Domain.MessageSource;

namespace Application.ChainOfResponsibilities.SourceFactory;

public class PhoneMessageSourceFactory : BaseMessageSourceFactory
{
    public override BaseMessageSource CreateMessageSource(string label)
    {
        return new PhoneMessageSource(Guid.NewGuid(), label);
    }
}
