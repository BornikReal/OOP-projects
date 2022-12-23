using Application.ChainOfResponsibilities.MessageFactory;
using Application.ChainOfResponsibilities.MessageModels;
using Domain.Messages;
using Domain.MessageSource;

namespace Application.ChainOfResponsibilities.MessageHandlerChain;

public class EmailMessageHandler : BaseMessageHandler
{
    public override BaseMessage? HandleRequest(BaseMessageModel model, Func<string, IReadOnlyCollection<BaseMessageSource>> getSources)
    {
        if (model is EmailMessageModel modelConcr)
        {
            var factory = new EmailMessageFactory(modelConcr.sender, modelConcr.messageSubject);
            return factory.CreateMessage(modelConcr.label, modelConcr.message, getSources);
        }
        else
        {
            return base.HandleRequest(model, getSources);
        }
    }
}