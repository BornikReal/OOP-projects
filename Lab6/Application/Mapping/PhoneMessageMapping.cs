using Application.Dto.Messages;
using Domain.Messages;

namespace Application.Mapping;

public static class PhoneMessageMapping
{
    public static PhoneMessageDto AsDto(this PhoneMessage phoneMessage)
        => new PhoneMessageDto(phoneMessage.Sender, phoneMessage.Message, phoneMessage.Id, phoneMessage.Label, phoneMessage.State);
}
