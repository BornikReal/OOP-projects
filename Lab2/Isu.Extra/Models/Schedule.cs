using Isu.Extra.Lessons;

namespace Isu.Extra.Models;

public class Schedule
{
    private readonly List<CertainLesson> _lessons;
    public Schedule(List<CertainLesson> lessons)
    {
        _lessons = lessons;
    }

    public IReadOnlyList<CertainLesson> Lessons => _lessons;

    public CertainLesson? FindLesson(Lesson lesson)
    {
        return _lessons.Find(s => s.Lesson == lesson);
    }
}
