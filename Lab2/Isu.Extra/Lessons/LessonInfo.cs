namespace Isu.Extra.Lessons;

public class LessonInfo
{
    public LessonInfo(LessonLocation lessonLocation, bool evenWeek, Weekend dayOfTheWeek, string teacher, DateTime timeStart, DateTime timeEnd)
    {
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

    public DateTime TimeStart { get; }
    public DateTime TimeEnd { get; }
}
