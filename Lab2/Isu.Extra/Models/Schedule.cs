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

    public static bool HaveIntersection(Schedule schedule1, Schedule schedule2)
    {
        return schedule1._lessons.Any(l1 => schedule2._lessons.Any(l2 => CertainLesson.HaveIntersection(l1, l2)));
    }

    public CertainLesson? FindLesson(Lesson lesson)
    {
        return _lessons.Find(s => s.Lesson == lesson);
    }
}
