using Application.ChainOfResponsibilities.SourceFactory;
using Application.ChainOfResponsibilities.MessageSourceModels;
using Domain.MessageSource;

namespace Application.ChainOfResponsibilities.SourceHandlerChain;

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