using Application.ChainOfResponsibilities.MessageFactory;
using Application.ChainOfResponsibilities.MessageModels;
using Domain.Messages;
using Domain.MessageSource;

namespace Application.ChainOfResponsibilities.MessageHandlerChain;

public class MessengerMessageHandler : BaseMessageHandler
{
    public override BaseMessage? HandleRequest(BaseMessageModel model, Func<string, IReadOnlyCollection<BaseMessageSource>> getSources)
    {
        if (model is EmailMessageModel modelConcr)
        {
            var factory = new MessengerMessageFactory(modelConcr.sender);
            return factory.CreateMessage(modelConcr.label, modelConcr.message, getSources);
        }
        else
        {
            return base.HandleRequest(model, getSources);
        }
    }
}