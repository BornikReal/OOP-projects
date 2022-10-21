namespace Isu.Extra.Exception;

public class InvalidTimeException : IsuExtraException
{
    public InvalidTimeException()
        : base("Start time of lesson can't be later then end time.")
    { }
}
