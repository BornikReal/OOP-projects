namespace Isu.Extra.Exception;

public class CGTAStudentException : IsuExtraException
{
    public CGTAStudentException(string name)
        : base($"Student {name} can't be removed or added to this CGTA Stream.")
    { }
}
