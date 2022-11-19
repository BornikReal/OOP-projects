namespace Backups.Strategy;

public class NowTimeStrategy : ITimeStrategy
{
    public DateTime SetTime()
    {
        return DateTime.Now;
    }
}
