namespace Banks.Notificators;

public interface INotificatorStrategy
{
    void Notify(string message);
}
