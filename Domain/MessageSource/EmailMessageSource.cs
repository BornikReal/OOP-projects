using System.Text.RegularExpressions;
using Domain.Messages;

namespace Domain.MessageSource;

public class EmailMessageSource : BaseMessageSource
{
    private static readonly Regex RegexEmail = new Regex(@"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+", RegexOptions.Compiled);

    public EmailMessageSource(Guid id, string label)
        : base(id, label)
    {
        if (!RegexEmail.Match(label).Success)
            throw new ArgumentException("Invalid email address", nameof(label));

        // TODO: I Blame you
        MessagesList = new List<EmailMessage>();
    }

#pragma warning disable CS8618
    protected EmailMessageSource() { }

    public override IReadOnlyCollection<BaseMessage> Messages
    {
        get => MessagesList;
        protected init => MessagesList = value.Select(x => (EmailMessage)x).ToList();
    }

    protected virtual List<EmailMessage> MessagesList { get; init; }

    public void AddMessage(EmailMessage message)
    {
        if (MessagesList.Contains(message))
            throw new InvalidOperationException("Message already exists.");
        MessagesList.Add(message);
    }
}
