using Isu.Extra.Lessons;
using Isu.Extra.SuperEntities;

namespace Isu.Extra.CGTA;

public class CGTAStream
{
    private readonly List<CertainLesson> _lessons;
    private readonly List<SuperStudent> _students;

    public CGTAStream(int maxSize, List<CertainLesson> lessons, List<SuperStudent> students, CGTACourse course, string streamName)
    {
        MaxSize = maxSize;
        _lessons = lessons;
        _students = students;
        Course = course;
        StreamName = streamName;
    }

    public int MaxSize { get; }
    public CGTACourse Course { get; }
    public string StreamName { get; }
    public IReadOnlyList<CertainLesson> Lessons => _lessons;
    public IReadOnlyList<SuperStudent> Students => _students;

    public CertainLesson? FindLesson(Lesson lesson)
    {
        return _lessons.Find(s => s.Lesson == lesson);
    }

    public void RemoveStudent(SuperStudent student)
    {
        if (!_students.Remove(student))
            throw new System.Exception();
    }

    public void AddStudent(SuperStudent student)
    {
        if (_students.Count >= MaxSize)
            throw new System.Exception();
        if (_students.Contains(student))
            throw new System.Exception();
        _students.Add(student);
    }
}
