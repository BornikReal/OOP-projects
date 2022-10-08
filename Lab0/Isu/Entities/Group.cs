using Isu.Exception;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    public const int MaxSize = 30;

    private readonly List<Student> _students;

    public Group(GroupName name)
    {
        Name = name;
        _students = new List<Student>();
    }

    public IReadOnlyList<Student> Students => _students;

    public GroupName Name { get; set; }

    public void Remove(Student student)
    {
        if (!_students.Remove(student))
            throw new StudentIdNotFoundException(student.Id);
    }

    public void Add(Student student)
    {
        if (_students.Count >= MaxSize)
            throw new GroupOverflowException(MaxSize);
        if (_students.Contains(student))
            throw new StudentAlreadyInGroupException(student.Name);
        _students.Add(student);
    }
}