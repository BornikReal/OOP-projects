namespace Isu.Exception;

public class StudentIdNotFoundException : IsuException
{
    public StudentIdNotFoundException(int id)
        : base($"Student with id {id} not found.")
    { }
}
