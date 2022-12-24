namespace Presentation.Models.MessageSourceModels;

public record CreateEmailMessageSourceModel(Guid accountId, string label) : CreateBaseMessageSourceModel(accountId, label);