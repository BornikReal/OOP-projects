namespace Isu.Extra.Lessons;

public class CertainLesson
{
    public CertainLesson(Lesson lesson, List<LessonInfo> info)
    {
        Lesson = lesson;
        Info = info;
    }

    public Lesson Lesson { get; }
    public List<LessonInfo> Info { get; } = new List<LessonInfo>();

    public static bool HaveIntersection(CertainLesson lesson1, CertainLesson lesson2)
    {
        foreach (LessonInfo info1 in lesson1.Info)
        {
            foreach (LessonInfo info2 in lesson2.Info)
            {
                if (LessonInfo.HaveIntersection(info1, info2))
                    return true;
            }
        }

        return false;
    }
}
