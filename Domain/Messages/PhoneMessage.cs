using System.Text;

namespace Domain.Messages;

public class PhoneMessage : BaseMessage
{
    public PhoneMessage(string phoneNumber, string message, Guid id, string label, MessageState state)
        : base(id, message, state, label)
    {
        PhoneNumber = phoneNumber;
        if (Encoding.Unicode.GetByteCount(message) > MessageSizeBytes)
        {
            throw new ArgumentException($"Message is too long. Max length is {MessageSizeBytes} bytes.");
        }
    }

#pragma warning disable CS8618
    protected PhoneMessage() { }

    public int MessageSizeBytes { get; } = 140;
    public string PhoneNumber { get; }
}
