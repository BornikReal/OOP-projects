using Application.Dto.Messages;
using Domain.Messages;

namespace Application.Mapping;

public static class EmailMessageMapping
{
    public static EmailMessageDto AsDto(this EmailMessage emailMessage)
        => new EmailMessageDto(emailMessage.Sender, emailMessage.MessageSubject, emailMessage.Message, emailMessage.Id, emailMessage.Label, emailMessage.State);
}
