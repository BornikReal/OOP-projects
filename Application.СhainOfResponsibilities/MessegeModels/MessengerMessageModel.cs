namespace Application.СhainOfResponsibilities.MessegeModels;

public record MessengerMessageModel(string label, string message, string sender)
    : BaseMessageModel(label, message);