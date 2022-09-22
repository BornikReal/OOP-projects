using Isu.Exception.InvalidGroupNameException;
using static Isu.Models.GroupNameParts.EduTypeNumber;

namespace Isu.Models.GroupNameParts;

public class CourseNumber
{
    public const int MaxMagCourse = 2;
    public const int MaxBachCourse = 5;
    public const int MaxSpecCourse = 6;
    public const int MaxPostgradDoctCourse = 9;

    public CourseNumber(GroupName groupName, int number = 1)
    {
        SetCourse(groupName, number);
    }

    public int Number { get; private set; }

    public void SetCourse(GroupName groupName, int number)
    {
        if (groupName.EduType.Number == Edu.MagId && (number < 1 || number > MaxMagCourse))
            throw new InvalidCourseNumberException(number);
        if (groupName.EduType.Number == Edu.BachId && (number < 1 || number > MaxBachCourse))
            throw new InvalidCourseNumberException(number);
        if (groupName.EduType.Number == Edu.SpecId && (number < 1 || number > MaxSpecCourse))
            throw new InvalidCourseNumberException(number);
        if ((groupName.EduType.Number == Edu.PostGradId || groupName.EduType.Number == Edu.DoctId) && (number < 1 || number > MaxPostgradDoctCourse))
            throw new InvalidCourseNumberException(number);
        Number = number;
    }

    public bool Equals(CourseNumber obj)
    {
        return Number == obj.Number;
    }
}