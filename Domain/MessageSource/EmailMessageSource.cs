using Domain.Messages;
using System.Text.RegularExpressions;

namespace Domain.MessageSource;

public class EmailMessageSource : BaseMessageSource
{
    private readonly HashSet<EmailMessage> _messages;
    private static readonly Regex RegexEmail = new Regex(@"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+", RegexOptions.Compiled);
    
    public EmailMessageSource(Guid id, string label) : base(id, label)
    {
        if (!RegexEmail.Match(label).Success)
            throw new ArgumentException("Invalid email address", nameof(label));
        _messages = new HashSet<EmailMessage>();
    }
    
    public override IReadOnlyCollection<IBaseMessage> Messages => _messages;

    public void AddMessage(EmailMessage message)
    {
        if (_messages.Add(message) is false)
            throw new InvalidOperationException("Message already exists.");
    }
}
