namespace Presentation.Models.MessageModels;

public record CreatePhoneMessageModel(string label, string message, string phoneNumber)
    : CreateBaseMessageModel(label, message);