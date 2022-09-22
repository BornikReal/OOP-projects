using Isu.Exception.InvalidGroupNameException;

namespace Isu.Models.GroupNameParts;

public class CourseNumber
{
    public const int MaxMagCourse = 2;
    public const int MaxBachCourse = 5;
    public const int MaxSpecCourse = 6;
    public const int MaxPostgradDoctCourse = 9;

    public CourseNumber(GroupName groupName, int number = 1)
    {
        if (groupName.EduType.Number == EduId.MagId && (number < 1 || number > MaxMagCourse))
            throw new InvalidCourseNumberException(number);
        if (groupName.EduType.Number == EduId.BachId && (number < 1 || number > MaxBachCourse))
            throw new InvalidCourseNumberException(number);
        if (groupName.EduType.Number == EduId.SpecId && (number < 1 || number > MaxSpecCourse))
            throw new InvalidCourseNumberException(number);
        if ((groupName.EduType.Number == EduId.PostGradId || groupName.EduType.Number == EduId.DoctId) && (number < 1 || number > MaxPostgradDoctCourse))
            throw new InvalidCourseNumberException(number);
        Number = number;
    }

    public int Number { get; }

    public bool Equals(CourseNumber obj)
    {
        return Number == obj.Number;
    }
}