namespace Application.СhainOfResponsibilities.MessegeModels;

public record EmailMessageModel(string label, string message, string sender, string messageSubject)
    : BaseMessageModel(label, message);