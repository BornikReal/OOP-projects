using Banks.AccountBuilders;
using Banks.BankBuilders;
using Banks.DateObservers;
using Banks.InterestRateStrategy;
using Banks.Models;
using Banks.PersonBuilder;
using Banks.Transactions;
using Xunit;

namespace Banks.Test;

public class BankTest
{
    [Fact]
    public void SimpleTest()
    {
        CentralBank.GetInstance(new DefaultClock());
        var dic = new Dictionary<decimal, decimal>
        {
            { 5, 5 },
        };
        var bankBuilder = new DefaultBankBuilder();
        bankBuilder.SetComissionRate(1)
            .SetCreditLimit(-5)
            .SetTransferLimit(1)
            .SetDepositSpan(TimeSpan.FromDays(1))
            .SetDebitInterestRate(1)
            .SetInterestRateStrategy(new DefaultInterestRateStrategy(dic));
        Bank bank = CentralBank.CreateBank(bankBuilder);
        IPerson person = new DefaultPersonBuilder()
            .SetName("Bornik")
            .SetSurname("Real")
            .SetAdress("Azov")
            .SetPassportData("1234567890")
            .Build();
        CentralBank.RegisterPerson(person);

        Guid id = bank.CreateBankAccount(new DebitAccountFactory(person));
        CentralBank.MakeTransaction(new DepositTransaction(id, 20));
        var t = new WithdrawTransaction(id, 10);
        CentralBank.MakeTransaction(t);
        Assert.Equal(10, CentralBank.GetMoney(id));
        CentralBank.CancelTransaction(t.TransactionId);
        Assert.Equal(20, CentralBank.GetMoney(id));
    }
}