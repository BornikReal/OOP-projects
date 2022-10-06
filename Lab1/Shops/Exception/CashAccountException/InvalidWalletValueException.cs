namespace Shops.Exception.CashAccountException;

public class InvalidWalletValueException : CashAccountException
{
    public InvalidWalletValueException(decimal value)
        : base($"Invalid wallet value: {value}!\nWallet value must be not negative number.")
    { }
}
