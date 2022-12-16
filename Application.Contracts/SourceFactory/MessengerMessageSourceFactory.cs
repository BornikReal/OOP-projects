using Domain.MessageSource;

namespace Application.Contracts.SourceFactory;

public class MessengerMessageSourceFactory : BaseMessageSourceFactory
{
    public MessengerMessageSourceFactory(string label)
        : base(label) { }

    public override BaseMessageSource CreateMessageSource()
    {
        return new MessengerMessageSource(Guid.NewGuid(), _label);
    }
}
