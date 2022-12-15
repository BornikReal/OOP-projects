﻿using System.Text.RegularExpressions;
using Domain.Messages;

namespace Domain.MessageSource;

public class PhoneMessageSource : BaseMessageSource
{
    private static readonly Regex RegexPhone = new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", RegexOptions.Compiled);
    private readonly HashSet<PhoneMessage> _messages;
    public PhoneMessageSource(Guid id, string label)
        : base(id, label)
    {
        if (!RegexPhone.Match(label).Success)
            throw new ArgumentException("Invalid phone number", nameof(label));
        _messages = new HashSet<PhoneMessage>();
    }

    public override IReadOnlyCollection<BaseMessage> Messages => _messages;

    public void AddMessage(PhoneMessage message)
    {
        if (_messages.Add(message) is false)
            throw new InvalidOperationException("Message already exists.");
    }
}