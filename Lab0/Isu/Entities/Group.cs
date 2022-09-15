using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private readonly List<Student> _students;

    public Group(GroupName name)
    {
        MaxSize = 30;
        Name = name;
        _students = new List<Student>();
    }

    public IReadOnlyList<Student> Students => _students;

    public int MaxSize { get; }

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
    }
}