namespace Presentation.Models.MessageSourceModels;

public record CreateMessengerMessageSourceModel(Guid accountId, string label) : CreateBaseMessageSourceModel(accountId, label);