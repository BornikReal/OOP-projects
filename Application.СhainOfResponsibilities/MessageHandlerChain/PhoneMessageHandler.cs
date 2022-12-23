using Application.СhainOfResponsibilities.MessageFactory;
using Application.СhainOfResponsibilities.MessageModels;
using Domain.Messages;
using Domain.MessageSource;

namespace Application.СhainOfResponsibilities.MessageHandlerChain;

public class PhoneMessageHandler : BaseMessageHandler
{
    public override BaseMessage? HandleRequest(BaseMessageModel model, Func<string, IReadOnlyCollection<BaseMessageSource>> getSources)
    {
        if (model is PhoneMessageModel modelConcr)
        {
            var factory = new PhoneMessageFactory(modelConcr.phoneNumber);
            return factory.CreateMessage(modelConcr.label, modelConcr.message, getSources);
        }
        else
        {
            return base.HandleRequest(model, getSources);
        }
    }
}