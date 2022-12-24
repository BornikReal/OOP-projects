using System.Text;
using Domain.Common.Exceptions;

namespace Domain.Messages;

public class PhoneMessage : BaseMessage
{
    public PhoneMessage(string phoneNumber, string message, Guid id, string label)
        : base(id, message, MessageState.New, label)
    {
        Sender = phoneNumber;
        if (Encoding.Unicode.GetByteCount(message) > MessageSizeBytes)
        {
            throw MessageException.PhoneMessageSizeExceed();
        }
    }

#pragma warning disable CS8618
    protected PhoneMessage() { }

    public static int MessageSizeBytes { get; } = 140;
    public string Sender { get; protected init; }
}
