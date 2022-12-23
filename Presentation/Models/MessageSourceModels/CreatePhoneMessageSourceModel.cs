namespace Presentation.Models.MessageSourceModels;

public record CreatePhoneMessageSourceModel(Guid accountId, string label) : CreateBaseMessageSourceModel(accountId, label);