namespace Isu.Extra.Models.LessonParts;

public class Lesson
{
    public Lesson(string name, LessonType lessonType)
    {
        Name = name;
        LessonType = lessonType;
    }

    public string Name { get; }
    public LessonType LessonType { get; }
}