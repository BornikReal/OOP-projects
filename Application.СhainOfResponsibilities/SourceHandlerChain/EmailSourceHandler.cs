using Application.ChainOfResponsibilities.SourceFactory;
using Application.ChainOfResponsibilities.MessageSourceModels;
using Domain.MessageSource;

namespace Application.ChainOfResponsibilities.SourceHandlerChain;

public class EmailSourceHandler : BaseSourceHandler
{
    public override BaseMessageSource? HandleRequest(BaseMessageSourceModel sourceModel)
    {
        if (sourceModel is EmailMessageSourceModel model)
        {
            var factory = new EmailMessageSourceFactory();
            return factory.CreateMessageSource(model.label);
        }
        else
        {
            return base.HandleRequest(sourceModel);
        }
    }
}