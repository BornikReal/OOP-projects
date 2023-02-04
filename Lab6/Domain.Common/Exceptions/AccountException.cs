namespace Domain.Common.Exceptions;

public class AccountException : DomainException
{
    private AccountException(string? message)
        : base(message) { }

    public static AccountException SourceAlreadyInAccount(Guid sourceId)
        => new AccountException($"Source {sourceId} is alreday in account");
}