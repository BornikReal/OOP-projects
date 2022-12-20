using System.Text.RegularExpressions;
using Domain.Messages;

namespace Domain.MessageSource;

public class PhoneMessageSource : BaseMessageSource
{
    private static readonly Regex RegexPhone = new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", RegexOptions.Compiled);

    public PhoneMessageSource(Guid id, string label)
        : base(id, label)
    {
        if (!RegexPhone.Match(label).Success)
            throw new ArgumentException("Invalid phone number", nameof(label));
        MessagesList = new List<PhoneMessage>();
    }

#pragma warning disable CS8618
    protected PhoneMessageSource() { }

    public override IReadOnlyCollection<BaseMessage> Messages
    {
        get => MessagesList;
        protected init => MessagesList = value.Select(x => (PhoneMessage)x).ToList();
    }

    protected virtual List<PhoneMessage> MessagesList { get; set; }

    public void AddMessage(PhoneMessage message)
    {
        if (MessagesList.Contains(message))
            throw new InvalidOperationException("Message already exists.");
        MessagesList.Add(message);
    }
}
