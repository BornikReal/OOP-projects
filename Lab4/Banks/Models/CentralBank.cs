using Banks.BankAccounts;
using Banks.BankBuilders;
using Banks.BankTransactions;
using Banks.DateObservers;
using Banks.Transactions;

namespace Banks.Models;

public class CentralBank
{
    private static readonly List<Bank> _banks = new List<Bank>();
    private static readonly List<IBankAccount> _accounts = new List<IBankAccount>();
    private static readonly List<BankTransaction> _transactions = new List<BankTransaction>();
    private static readonly List<IPerson> _persons = new List<IPerson>();
    private static IClock? _clock;
    private static CentralBank? _instance;

    private CentralBank(IClock clock)
    {
        _clock = clock;
    }

    public static CentralBank GetInstance(IClock clock)
    {
        _instance ??= new CentralBank(clock);
        return _instance;
    }

    public static Bank CreateBank(IBankBuilder builder)
    {
        builder.SetClock(_clock!);
        Bank bank = builder.Build();
        bank.Notify += OnBankAccountCreated;
        _banks.Add(bank);
        return bank;
    }

    public static void MakeTransaction(IAccountTransaction transaction)
    {
        var visitor = new DefaultTransactionVisitor(_accounts);
        transaction.Accept(visitor);
        _transactions.Add(visitor.BankTransaction!);
    }

    public static void AddDays(int days)
    {
        if (days <= 0)
            throw new ArgumentException("Days must be positive");
        _clock !.AddTime(TimeSpan.FromDays(days));
    }

    public static void RegisterPerson(IPerson person)
    {
        _persons.Add(person);
    }

    public static Bank GetBank(Guid id)
    {
        return _banks.Find(bank => bank.Id == id) !;
    }

    public static IPerson GetPerson(Guid id)
    {
        return _persons.Find(person => person.Id == id) !;
    }

    private static void OnBankAccountCreated(IBankAccount account)
    {
        _accounts.Add(account);
    }
}
