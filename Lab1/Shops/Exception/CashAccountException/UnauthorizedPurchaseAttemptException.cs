namespace Shops.Exception.CashAccountException;

public class UnauthorizedPurchaseAttemptException : CashAccountException
{
    public UnauthorizedPurchaseAttemptException()
        : base($"Shop didn't get purchase authorization from CashAccount!")
    { }
}
