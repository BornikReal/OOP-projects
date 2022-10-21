using Isu.Extra.Models;

namespace Isu.Extra.Builders;

public class ScheduleBuilder
{
    private readonly List<CertainLesson> _lessons = new List<CertainLesson>();

    public void AddNewLesson(CertainLesson newLesson)
    {
        foreach (CertainLesson lesson in _lessons)
        {
            if (CertainLesson.HaveIntersection(newLesson, lesson))
                throw new System.Exception();
        }

        _lessons.Add(newLesson);
    }

    public void Reset()
    {
        _lessons.Clear();
    }

    public Schedule GetLessons()
    {
        return new Schedule(_lessons);
    }
}