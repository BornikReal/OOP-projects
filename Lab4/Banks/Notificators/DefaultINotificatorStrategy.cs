using Banks.Models;

namespace Banks.Notificators;

public class DefaultINotificatorStrategy : INotificatorStrategy
{
    private readonly IPerson _person;
    private readonly Action<IPerson, string> _notificator;

    public DefaultINotificatorStrategy(IPerson person, Action<IPerson, string> notificator)
    {
        _person = person;
        _notificator = notificator;
    }

    public void Notify(string message)
    {
        _notificator.Invoke(_person, message);
    }
}
