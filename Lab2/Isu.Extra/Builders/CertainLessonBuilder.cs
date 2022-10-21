using Isu.Extra.Models;
using Isu.Extra.Models.LessonParts;

namespace Isu.Extra.Builders;

public class CertainLessonBuilder
{
    private readonly List<LessonInfo> _infos = new List<LessonInfo>();
    private Lesson? _lesson;

    public void SetLesson(Lesson lesson)
    {
        _lesson = lesson;
    }

    public void AddNewInfo(LessonLocation lessonLocation, bool evenWeek, Weekend dayOfTheWeek, string teacher, TimeOnly timeStart, TimeOnly timeEnd)
    {
        var newLessonInfo = new LessonInfo(lessonLocation, evenWeek, dayOfTheWeek, teacher, timeStart, timeEnd);
        foreach (LessonInfo info in _infos)
        {
            if (LessonInfo.HaveIntersection(info, newLessonInfo))
                throw new System.Exception();
        }

        _infos.Add(newLessonInfo);
    }

    public CertainLesson GetCertainLesson()
    {
        if (_lesson == null)
            throw new System.Exception();
        return new CertainLesson(_lesson!, _infos);
    }

    public void Reset()
    {
        _infos.Clear();
        _lesson = null;
    }
}
