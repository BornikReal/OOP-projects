using Isu.Exception;

namespace Isu.Models;

public class CourseNumber
{
    private int _number;
    public CourseNumber(int number = 1)
    {
        if (number is < 1 or > 9)
            throw new InvalidCourseNumberException(number);
        Number = number;
    }

    public int Number
    {
        get => _number;
        set
        {
            if (value is > 9 or < 1)
                throw new InvalidCourseNumberException(value);

            _number = value;
        }
    }
}