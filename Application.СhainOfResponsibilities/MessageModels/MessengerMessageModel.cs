namespace Application.СhainOfResponsibilities.MessageModels;

public record MessengerMessageModel(string label, string message, string sender)
    : BaseMessageModel(label, message);