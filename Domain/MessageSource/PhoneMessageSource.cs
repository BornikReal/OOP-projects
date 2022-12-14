using Domain.Messages;
using System.Text.RegularExpressions;

namespace Domain.MessageSource;

public class PhoneMessageSource : IMessageSource
{
    private readonly HashSet<PhoneMessage> _messages;
    private static readonly Regex RegexPhone = new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", RegexOptions.Compiled);
    public PhoneMessageSource(Guid id, string label)
    {
        Id = id;
        if (!RegexPhone.Match(label).Success)
            throw new ArgumentException("Invalid phone number", nameof(label));
        Label = label;
        _messages = new HashSet<PhoneMessage>();
    }

    public Guid Id { get; }
    public IReadOnlyCollection<IBaseMessage> Messages => _messages;
    public string Label { get; }

    public void AddMessage(PhoneMessage message)
    {
        if (_messages.Add(message) is false)
            throw new InvalidOperationException("Message already exists.");
    }
}
