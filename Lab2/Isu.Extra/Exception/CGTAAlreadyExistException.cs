namespace Isu.Extra.Exception;

internal class CGTAAlreadyExistException : IsuExtraException
{
    public CGTAAlreadyExistException(string name)
        : base($"CGTA with name {name} already exist in this CGTA.")
    { }
}
