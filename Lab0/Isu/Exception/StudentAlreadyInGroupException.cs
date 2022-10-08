namespace Isu.Exception;

public class StudentAlreadyInGroupException : IsuException
{
    public StudentAlreadyInGroupException(string name)
        : base($"Student {name} already in this group.")
    { }
}
