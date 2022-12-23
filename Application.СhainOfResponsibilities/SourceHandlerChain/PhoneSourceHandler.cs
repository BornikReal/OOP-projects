using Application.СhainOfResponsibilities.SourceFactory;
using Application.СhainOfResponsibilities.MessageSourceModels;
using Domain.MessageSource;

namespace Application.СhainOfResponsibilities.SourceHandlerChain;

public class PhoneSourceHandler : BaseSourceHandler
{
    public override BaseMessageSource? HandleRequest(BaseMessageSourceModel sourceModel)
    {
        if (sourceModel is PhoneMessageSourceModel model)
        {
            var factory = new PhoneMessageSourceFactory();
            return factory.CreateMessageSource(model.label);
        }
        else
        {
            return base.HandleRequest(sourceModel);
        }
    }
}