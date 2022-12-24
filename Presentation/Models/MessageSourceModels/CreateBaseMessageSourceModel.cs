namespace Presentation.Models.MessageSourceModels;

public abstract record CreateBaseMessageSourceModel(Guid accountId, string label);