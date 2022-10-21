using Isu.Extra.Exception;
using Isu.Extra.Models;
using Isu.Extra.SuperEntities;

namespace Isu.Extra.CGTA;

public class CGTAStream
{
    private readonly List<SuperStudent> _students = new List<SuperStudent>();

    public CGTAStream(string streamName, int maxSize, Schedule lessons, CGTACourse course)
    {
        MaxSize = maxSize;
        Lessons = lessons;
        Course = course;
        StreamName = streamName;
    }

    public int MaxSize { get; }
    public CGTACourse Course { get; }
    public string StreamName { get; }
    public Schedule Lessons { get; }
    public IReadOnlyList<SuperStudent> Students => _students;

    public void RemoveStudent(SuperStudent student)
    {
        if (!_students.Remove(student))
            throw new CGTAStudentException(student.Student.Name);
    }

    public void AddStudent(SuperStudent student)
    {
        if (_students.Count >= MaxSize)
            throw new CGTAStudentException(student.Student.Name);
        if (_students.Contains(student))
            throw new CGTAStudentException(student.Student.Name);
        _students.Add(student);
    }
}
