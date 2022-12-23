using System.Text.RegularExpressions;
using Domain.Common.Exceptions;
using Domain.Messages;

namespace Domain.MessageSource;

public class EmailMessageSource : BaseMessageSource
{
    private static readonly Regex RegexEmail = new Regex(@"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+", RegexOptions.Compiled);
    private List<BaseMessage> _messages = new List<BaseMessage>();

    public EmailMessageSource(Guid id, string label)
        : base(id, label)
    {
        if (!RegexEmail.Match(label).Success)
            throw MessageSourceException.InvalidEmailAddress(label);
    }

#pragma warning disable CS8618
    protected EmailMessageSource() { }

    public override IReadOnlyCollection<BaseMessage> Messages
    {
        get => _messages;
        protected init => _messages = value.ToList();
    }

    public void AddMessage(EmailMessage message)
    {
        if (_messages.Contains(message))
            throw MessageSourceException.MessageAlreadyExistInSource(Id);
        _messages.Add(message);
    }
}
