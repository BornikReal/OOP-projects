using Backups.Strategy;

namespace Backups.Extra;

public class UserTimeStrategy : ITimeStrategy
{
    public DateTime Time { get; set; }
    public DateTime SetTime()
    {
        return Time;
    }
}
