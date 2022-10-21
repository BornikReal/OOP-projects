using Isu.Extra.Models.LessonParts;

namespace Isu.Extra.Models;

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
        return lesson1.Info.Any(l1 => lesson2.Info.Any(l2 => LessonInfo.HaveIntersection(l1, l2)));
    }
}
