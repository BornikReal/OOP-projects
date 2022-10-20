using Isu.Extra.Lessons;

namespace Isu.Extra.Models;

public class GroupSchedule
{
    private readonly List<ConcreteLesson> _lessons;
    public GroupSchedule(List<ConcreteLesson> lessons)
    {
        _lessons = lessons;
    }

    public IReadOnlyList<ConcreteLesson> Lessons => _lessons;

    public ConcreteLesson? FindLesson(Lesson lesson)
    {
        return _lessons.Find(s => s.Lesson == lesson);
    }
}
