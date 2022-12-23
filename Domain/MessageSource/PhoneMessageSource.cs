using System.Text.RegularExpressions;
using Domain.Common.Exceptions;
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
            throw MessageSourceException.InvalidPhoneNumber(label);
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
            throw MessageSourceException.MessageAlreadyExistInSource(Id);
        _messages.Add(message);
    }
}
