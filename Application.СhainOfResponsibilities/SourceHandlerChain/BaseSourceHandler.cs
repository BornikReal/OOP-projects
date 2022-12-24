using Application.ChainOfResponsibilities.MessageSourceModels;
using Domain.MessageSource;

namespace Application.ChainOfResponsibilities.SourceHandlerChain;

public abstract class BaseSourceHandler
{
    private BaseSourceHandler? _nextHandler;

    public BaseSourceHandler SetNext(BaseSourceHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public virtual BaseMessageSource? HandleRequest(BaseMessageSourceModel sourceModel)
    {
        return _nextHandler?.HandleRequest(sourceModel);
    }
}
