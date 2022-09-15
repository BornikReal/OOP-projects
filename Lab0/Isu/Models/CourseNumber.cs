using Isu.Exception;
using static Isu.Models.EduTypeNumber;
using static Isu.Models.GroupName;

namespace Isu.Models;

public class CourseNumber
{
    private int _number;
    public CourseNumber(GroupName group_name, int number = 1)
    {
        SetCourse(group_name, number);
    }

    public int Number { get { return _number; } }

    public void SetCourse(GroupName group_name, int number)
    {
        if (group_name.EduType.Number == Edu.MagId && (number < 1 || number > MaxMagCourse))
            throw new InvalidCourseNumberException(number);
        if (group_name.EduType.Number == Edu.BachId && (number < 1 || number > MaxBachCourse))
            throw new InvalidCourseNumberException(number);
        if (group_name.EduType.Number == Edu.SpecId && (number < 1 || number > MaxSpecCourse))
            throw new InvalidCourseNumberException(number);
        if ((group_name.EduType.Number == Edu.PostGradId || group_name.EduType.Number == Edu.DoctId) && (number < 1 || number > MaxPDCourse))
            throw new InvalidCourseNumberException(number);
        _number = number;
    }
}