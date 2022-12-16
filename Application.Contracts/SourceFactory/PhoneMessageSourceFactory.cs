using Domain.MessageSource;

namespace Application.Contracts.SourceFactory;

public class PhoneMessageSourceFactory : BaseMessageSourceFactory
{
    public PhoneMessageSourceFactory(string label)
        : base(label) { }

    public override BaseMessageSource CreateMessageSource()
    {
        return new PhoneMessageSource(Guid.NewGuid(), _label);
    }
}
