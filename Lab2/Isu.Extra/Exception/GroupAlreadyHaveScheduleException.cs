namespace Isu.Extra.Exception;

public class GroupAlreadyHaveScheduleException : IsuExtraException
{
    public GroupAlreadyHaveScheduleException(string name)
        : base($"Group {name} already have schedule.")
    { }
}
