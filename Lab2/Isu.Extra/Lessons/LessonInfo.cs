namespace Isu.Extra.Lessons;

public class LessonInfo
{
    public LessonInfo(LessonLocation lessonLocation, bool evenWeek, Weekend dayOfTheWeek, string teacher, TimeOnly timeStart, TimeOnly timeEnd)
    {
        if (timeEnd <= timeStart)
            throw new System.Exception();
        LessonLocation = lessonLocation;
        EvenWeek = evenWeek;
        DayOfTheWeek = dayOfTheWeek;
        Teacher = teacher;
        TimeStart = timeStart;
        TimeEnd = timeEnd;
    }

    public LessonLocation LessonLocation { get; }
    public bool EvenWeek { get; }
    public Weekend DayOfTheWeek { get; }
    public string Teacher { get; }

    public TimeOnly TimeStart { get; }
    public TimeOnly TimeEnd { get; }

    public static bool HaveIntersection(LessonInfo info1, LessonInfo info2)
    {
        if (info1.EvenWeek != info2.EvenWeek || info1.DayOfTheWeek != info2.DayOfTheWeek)
            return false;
        if (info2.TimeStart >= info1.TimeStart && info2.TimeStart < info1.TimeEnd)
            return true;
        if (info1.TimeStart >= info2.TimeStart && info1.TimeStart < info2.TimeEnd)
             return true;
        return false;
    }
}
