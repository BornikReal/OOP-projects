using System.Text;

namespace Domain.Messages;

public class PhoneMessage : BaseMessage
{
    public PhoneMessage(string phoneNumber, string message, Guid id) : base(id)
    {
        PhoneNumber = phoneNumber;
        if (Encoding.Unicode.GetByteCount(message) > MessageSizeBytes)
        {
            throw new ArgumentException($"Message is too long. Max length is {MessageSizeBytes} bytes.");
        }
        
        Message = message;
    }
    
    public int MessageSizeBytes { get; } = 140;
    public string PhoneNumber { get; }
    public string Message { get; }
    public override string GetMessage()
    {
        return Message;
    }
}
