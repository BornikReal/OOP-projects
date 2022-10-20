using Isu.Extra.Lessons;

namespace Isu.Extra.Models;

public class StudentSchedule
{
    private readonly List<ConcreteLesson> _lessons;

    public StudentSchedule(List<ConcreteLesson> lessons, GroupSchedule groupSchedule)
    {
        _lessons = lessons;
        GroupSchedule = groupSchedule;
    }

    public GroupSchedule GroupSchedule { get; }

    public IReadOnlyList<ConcreteLesson> Lessons => _lessons;

    public void Unsubscribe(Lesson lesson)
    {
        ConcreteLesson? sear = _lessons.Find(s => s.Lesson == lesson);
        if (sear == null)
            throw new System.Exception();
        _lessons.Remove(sear);
    }

    public void Suscribe(Lesson lesson)
    {
        if (_lessons.Find(s => s.Lesson == lesson) != null)
            throw new System.Exception();
    }
}
