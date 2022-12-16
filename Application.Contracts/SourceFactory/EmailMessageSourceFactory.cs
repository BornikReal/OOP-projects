using Domain.MessageSource;

namespace Application.Contracts.SourceFactory;

public class EmailMessageSourceFactory : BaseMessageSourceFactory
{
    public EmailMessageSourceFactory(string label)
        : base(label) { }


    public override BaseMessageSource CreateMessageSource()
    {
        return new EmailMessageSource(Guid.NewGuid(), _label);
    }
}
