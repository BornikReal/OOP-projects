namespace Presentation.Models.MessageModels;

public record CreateMessengerMessageModel(string label, string message, string sender)
    : CreateBaseMessageModel(label, message);