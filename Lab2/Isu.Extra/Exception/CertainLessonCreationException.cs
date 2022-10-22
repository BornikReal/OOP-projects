namespace Isu.Extra.Exception;

public class CertainLessonCreationException : IsuExtraException
{
    public CertainLessonCreationException()
        : base("Certain lesson can't be created without his name.")
    { }
}
