using Isu.Entities;
using Isu.Exception;
using Isu.Models;
using Isu.Models.GroupNameParts;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Student> _students;
    private readonly List<Group> _groups;

    public IsuService()
    {
        _students = new List<Student>();
        _groups = new List<Group>();
    }

    public IReadOnlyList<Student> Students => _students;

    public IReadOnlyList<Group> Groups => _groups;

    public Group AddGroup(GroupName name)
    {
        if (FindGroup(name) != null)
            throw new GroupAlreadyExistException(name);
        _groups.Add(new Group(name));
        return _groups.Last();
    }

    public Student AddStudent(Group group, string name)
    {
        _students.Add(new Student(name, group));
        return _students.Last();
    }

    public Student? FindStudent(int id)
    {
        return _students.Find(s => s.Id == id);
    }

    public Student GetStudent(int id)
    {
        return _students.Find(s => s.Id == id) ?? throw new StudentIdNotFoundException(id);
    }

    public IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        Group? group = FindGroup(groupName);
        if (group == null)
            return new List<Student>();
        return group.Students;
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        List<Group> groups = FindGroups(courseNumber);
        return groups.SelectMany(x => x.Students).ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.Find(g => g.Name.Equals(groupName));
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups.FindAll(g => g.Name.Course.Equals(courseNumber));
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        student.ChangeGroup(newGroup);
    }
}
