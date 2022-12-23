using Application.СhainOfResponsibilities.MessageFactory;
using Application.СhainOfResponsibilities.MessageModels;
using Domain.Messages;
using Domain.MessageSource;

namespace Application.СhainOfResponsibilities.MessageHandlerChain;

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