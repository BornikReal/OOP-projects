using Backups.Models;

namespace Backups.Extra.Cleaner;

public class NumberCleaner : ICleaner
{
    private readonly int _number;
    public NumberCleaner(int number)
    {
        _number = number;
    }

    public IEnumerable<RestorePoint> Clean(IEnumerable<RestorePoint> restorePoints)
    {
        if (restorePoints.Count() <= _number)
            return new List<RestorePoint>();
        return restorePoints.Take(_number);
    }

    public override string ToString()
    {
        return "Number Cleaner";
    }
}
