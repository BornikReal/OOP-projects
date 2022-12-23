namespace Application.ChainOfResponsibilities.MessageModels;

public record MessengerMessageModel(string label, string message, string sender)
    : BaseMessageModel(label, message);