using Application.Dto.Messages;
using Domain.Messages;

namespace Application.Mapping;

public static class MessengerMessageMapping
{
    public static MessengerMessageDto AsDto(this MessengerMessage messengerMessage)
        => new MessengerMessageDto(messengerMessage.Sender, messengerMessage.Message, messengerMessage.Id, messengerMessage.Label, messengerMessage.State);
}
