namespace Isu.Extra.Lessons;

public class CertainLesson
{
    public CertainLesson(Lesson lesson, List<LessonInfo> info)
    {
        Lesson = lesson;
        Info = info;
    }

    public Lesson Lesson { get; }
    public List<LessonInfo> Info { get; }

    public static bool HaveIntersection(CertainLesson lesson1, CertainLesson lesson2)
    {
        foreach (LessonInfo info1 in lesson1.Info)
        {
            foreach (LessonInfo info2 in lesson2.Info)
            {
                if (info1.EvenWeek != info2.EvenWeek || info1.DayOfTheWeek != info2.DayOfTheWeek)
                    continue;
                if (info2.TimeStart >= info1.TimeStart && info2.TimeStart <= info1.TimeEnd)
                    return false;
                if (info1.TimeStart >= info2.TimeStart && info1.TimeStart <= info2.TimeEnd)
                    return false;
            }
        }

        return true;
    }
}
