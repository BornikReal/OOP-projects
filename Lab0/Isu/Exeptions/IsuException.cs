namespace Isu.Exception;

public abstract class IsuException : IOException
{
    public IsuException(string message)
        : base(message)
    { }
}

public class StudentIdNotFoundException : IsuException
{
    public StudentIdNotFoundException(int id)
        : base($"Student with id {id} not found.")
    { }
}

public abstract class InvalidGroupNameException : IsuException
{
    public InvalidGroupNameException(string message)
        : base(message)
    { }
}

public class InvalidFormatGroupNameException : InvalidGroupNameException
{
    public InvalidFormatGroupNameException(string message)
        : base($"\"{message}\" is invalid Group Name.")
    { }
}

public class FrongGroupInfoException : InvalidGroupNameException
{
    public FrongGroupInfoException(string field)
        : base($"Group field {field} is out of range or have incompatible data.")
    { }
}

public class InvalidCourseNumberException : IsuException
{
    public InvalidCourseNumberException(int number)
        : base($"Group number {number} is out of range.")
    { }
}

public class UnavailableIdException : IsuException
{
    public UnavailableIdException(int id)
        : base($"Id {id} has already used or out of range.")
    { }
}