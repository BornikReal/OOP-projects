namespace Application.ChainOfResponsibilities.MessageSourceModels;

public record EmailMessageSourceModel(string label) : BaseMessageSourceModel(label);