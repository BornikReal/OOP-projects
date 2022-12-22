using Application.СhainOfResponsibilities.SourceFactory;
using Application.СhainOfResponsibilities.MessageSourceModels;
using Domain.MessageSource;

namespace Application.СhainOfResponsibilities.SourceHandlerChain;

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