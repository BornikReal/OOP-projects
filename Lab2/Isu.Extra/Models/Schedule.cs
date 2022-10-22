using Isu.Extra.Exception;
using Isu.Extra.Models.LessonParts;

namespace Isu.Extra.Models;

public class Schedule
{
    private readonly List<CertainLesson> _lessons;
    private Schedule(List<CertainLesson> lessons)
    {
        _lessons = lessons;
    }

    public static ScheduleBuilder Builder => new ScheduleBuilder();
    public IReadOnlyList<CertainLesson> Lessons => _lessons;

    public static bool HaveIntersection(Schedule schedule1, Schedule schedule2)
    {
        return schedule1._lessons.Any(l1 => schedule2._lessons.Any(l2 => CertainLesson.HaveIntersection(l1, l2)));
    }

    public CertainLesson? FindLesson(Lesson lesson)
    {
        return _lessons.Find(s => s.Lesson == lesson);
    }

    public class ScheduleBuilder
    {
        private readonly List<CertainLesson> _lessons = new List<CertainLesson>();

        public ScheduleBuilder AddNewLesson(CertainLesson newLesson)
        {
            foreach (CertainLesson lesson in _lessons)
            {
                if (CertainLesson.HaveIntersection(newLesson, lesson))
                    throw new LessonsIntersectionException();
            }

            _lessons.Add(newLesson);
            return this;
        }

        public void Reset()
        {
            _lessons.Clear();
        }

        public Schedule Build()
        {
            return new Schedule(new List<CertainLesson>(_lessons));
        }
    }
}
