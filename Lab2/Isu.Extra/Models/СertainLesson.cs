using Isu.Extra.Exception;
using Isu.Extra.Models.LessonParts;

namespace Isu.Extra.Models;

public class CertainLesson
{
    private CertainLesson(Lesson lesson, List<LessonInfo> info)
    {
        Lesson = lesson;
        Info = info;
    }

    public static CertainLessonBuilder Builder => new CertainLessonBuilder();
    public Lesson Lesson { get; }
    public List<LessonInfo> Info { get; } = new List<LessonInfo>();

    public static bool HaveIntersection(CertainLesson lesson1, CertainLesson lesson2)
    {
        return lesson1.Info.Any(l1 => lesson2.Info.Any(l2 => LessonInfo.HaveIntersection(l1, l2)));
    }

    public class CertainLessonBuilder
    {
        private readonly List<LessonInfo> _infos = new List<LessonInfo>();
        private Lesson? _lesson;

        public CertainLessonBuilder SetLesson(Lesson lesson)
        {
            _lesson = lesson;
            return this;
        }

        public CertainLessonBuilder AddNewInfo(LessonLocation lessonLocation, bool evenWeek, Weekend dayOfTheWeek, string teacher, TimeOnly timeStart, TimeOnly timeEnd)
        {
            var newLessonInfo = new LessonInfo(lessonLocation, evenWeek, dayOfTheWeek, teacher, timeStart, timeEnd);
            foreach (LessonInfo info in _infos)
            {
                if (LessonInfo.HaveIntersection(info, newLessonInfo))
                    throw new LessonsIntersectionException();
            }

            _infos.Add(newLessonInfo);
            return this;
        }

        public CertainLesson Build()
        {
            if (_lesson == null)
                throw new CertainLessonCreationException();
            return new CertainLesson(_lesson, new List<LessonInfo>(_infos));
        }

        public void Reset()
        {
            _infos.Clear();
            _lesson = null;
        }
    }
}
