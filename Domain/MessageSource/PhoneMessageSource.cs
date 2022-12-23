using System.Text.RegularExpressions;
using Domain.Messages;

namespace Domain.MessageSource;

public class PhoneMessageSource : BaseMessageSource
{
    private static readonly Regex RegexPhone = new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", RegexOptions.Compiled);
    private List<BaseMessage> _messages = new List<BaseMessage>();

    public PhoneMessageSource(Guid id, string label)
        : base(id, label)
    {
        if (!RegexPhone.Match(label).Success)
            throw new ArgumentException("Invalid phone number", nameof(label));
    }

    protected PhoneMessageSource() { }

    public override IReadOnlyCollection<BaseMessage> Messages
    {
        get => _messages;
        protected init => _messages = value.ToList();
    }

    public void AddMessage(PhoneMessage message)
    {
        if (_messages.Contains(message))
            throw new InvalidOperationException("Message already exists.");
        _messages.Add(message);
    }
}
