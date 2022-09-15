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
        if (student.Group == null)
            _students.Remove(student);
    }

    public void Add(Student student)
    {
        if (student.Group == null)
            _students.Add(student);
        if (_students.Count > MaxSize)
            throw new ArgumentException();
    }
}