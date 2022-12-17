using System.Text.RegularExpressions;
using Domain.Messages;

namespace Domain.MessageSource;

public class EmailMessageSource : BaseMessageSource
{
    private static readonly Regex RegexEmail = new Regex(@"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+", RegexOptions.Compiled);
    private readonly List<EmailMessage> _messages;

    public EmailMessageSource(Guid id, string label)
        : base(id, label)
    {
        if (!RegexEmail.Match(label).Success)
            throw new ArgumentException("Invalid email address", nameof(label));
        _messages = new List<EmailMessage>();
    }

#pragma warning disable CS8618
    protected EmailMessageSource() { }

    public override IReadOnlyCollection<BaseMessage> Messages => _messages;

    public void AddMessage(EmailMessage message)
    {
        if (_messages.Contains(message))
            throw new InvalidOperationException("Message already exists.");
        _messages.Add(message);
    }
}
