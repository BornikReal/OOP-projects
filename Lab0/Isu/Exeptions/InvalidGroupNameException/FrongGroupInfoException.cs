namespace Isu.Exception.InvalidGroupNameException;

public class FrongGroupInfoException : InvalidGroupNameException
{
    public FrongGroupInfoException(string field)
        : base($"Group field {field} is out of range or have incompatible data.")
    { }
}
