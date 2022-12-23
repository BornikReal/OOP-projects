namespace Application.СhainOfResponsibilities.MessageModels;

public record PhoneMessageModel(string label, string message, string phoneNumber)
    : BaseMessageModel(label, message);