using Application.ChainOfResponsibilities.MessageModels;
using Domain.Messages;
using Domain.MessageSource;

namespace Application.ChainOfResponsibilities.MessageHandlerChain;

public class BaseMessageHandler
{
    private BaseMessageHandler? _nextHandler;

    public BaseMessageHandler SetNext(BaseMessageHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public virtual BaseMessage? HandleRequest(BaseMessageModel model, Func<string, IReadOnlyCollection<BaseMessageSource>> getSources)
    {
        return _nextHandler?.HandleRequest(model, getSources);
    }
}