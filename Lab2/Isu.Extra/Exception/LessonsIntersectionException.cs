namespace Isu.Extra.Exception;

public class LessonsIntersectionException : IsuExtraException
{
    public LessonsIntersectionException()
        : base("Lessons can't be created because of the intersection between them.")
    { }
}
