using Isu.Models;

namespace Isu.Exception;

public class GroupAlreadyExistException : IsuException
{
    public GroupAlreadyExistException(GroupName name)
        : base($"A group with the name {name} already exists.")
    { }
}