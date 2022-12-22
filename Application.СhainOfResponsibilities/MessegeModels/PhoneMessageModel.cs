namespace Application.СhainOfResponsibilities.MessegeModels;

public record PhoneMessageModel(string label, string message, string phoneNumber)
    : BaseMessageModel(label, message);