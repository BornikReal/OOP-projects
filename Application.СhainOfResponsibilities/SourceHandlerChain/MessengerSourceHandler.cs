using Application.ChainOfResponsibilities.SourceFactory;
using Application.ChainOfResponsibilities.MessageSourceModels;
using Domain.MessageSource;

namespace Application.ChainOfResponsibilities.SourceHandlerChain;

public class MessengerSourceHandler : BaseSourceHandler
{
    public override BaseMessageSource? HandleRequest(BaseMessageSourceModel sourceModel)
    {
        if (sourceModel is MessengerMessageSourceModel model)
        {
            var factory = new MessengerMessageSourceFactory();
            return factory.CreateMessageSource(model.label);
        }
        else
        {
            return base.HandleRequest(sourceModel);
        }
    }
}