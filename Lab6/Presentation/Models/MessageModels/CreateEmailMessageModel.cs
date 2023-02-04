namespace Presentation.Models.MessageModels;

public record CreateEmailMessageModel(string label, string message, string sender, string messageSubject)
    : CreateBaseMessageModel(label, message);