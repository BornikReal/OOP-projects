using Isu.Exception.InvalidGroupNameException;
using static Isu.Models.EduTypeNumber;

namespace Isu.Models;

public class GroupName
{
    public const int MaxMagCourse = 2;
    public const int MaxBachCourse = 5;
    public const int MaxSpecCourse = 6;
    public const int MaxPDCourse = 9;

    public const int MaxGroupNumBMS = 99;
    public const int MaxGroupNumPD = 999;

    public const char PDLetter = ' ';
    public const int NoneSpec = 0;

    public const int PDGroupNameLen = 4;
    public const int BMSGroupNameLenNoSpec = 5;
    public const int BMSGroupNameLen = 6;

    private GroupLetter _letter;
    private EduTypeNumber _edu_type;
    private CourseNumber _course;
    private GroupNumber _number;
    private SpecNumber _spec;

    public GroupName(string name)
    {
        GroupName new_group = Parse(name);
        (_letter, _edu_type, _course, _number, _spec) = (new_group._letter, new_group._edu_type, new_group._course, new_group._number, new_group._spec);
    }

    public GroupName(GroupLetter letter, EduTypeNumber edu_type, CourseNumber course, GroupNumber number, SpecNumber spec)
    {
        (_letter, _edu_type, _number, _spec, _course) = (letter, edu_type, number, spec, course);
    }

    private GroupName()
    {
        _letter = new GroupLetter();
        _edu_type = new EduTypeNumber(this);
        _course = new CourseNumber(this);
        _number = new GroupNumber(this);
        _spec = new SpecNumber(this);
    }

    public GroupLetter Letter { get => _letter; }

    public EduTypeNumber EduType { get => _edu_type; }

    public CourseNumber Course { get => _course; }

    public GroupNumber Number { get => _number; }

    public SpecNumber Spec { get => _spec; }

    public static GroupName Parse(string input)
    {
        // var groupRegex = new Regex(@"[A-Z]\d{3}[a-z]?$");
        var new_group = new GroupName();
        int x;
        bool success;
        if (input.Length == PDGroupNameLen)
        {
            new_group._letter.SetLetter(PDLetter);

            if (input[0] is not (char)(Edu.PostGradId + '0') and not (char)(Edu.DoctId + '0'))
                throw new InvalidFormatGroupNameException(input);
            new_group._edu_type.SetNumber(new_group, (Edu)(input[0] - '0'));

            new_group._course.SetCourse(new_group, 7);

            success = int.TryParse(input[1..], out x);
            if (!success)
                throw new InvalidFormatGroupNameException(input);
            new_group._number.SetNumber(new_group, x);

            new_group._spec.SetNumber(new_group, NoneSpec);
            return new_group;
        }
        else if (input.Length is BMSGroupNameLenNoSpec or BMSGroupNameLen)
        {
            new_group._letter.SetLetter(input[0]);

            if (input[1] is not (char)(Edu.BachId + '0') and not (char)(Edu.SpecId + '0') and not (char)(Edu.MagId + '0'))
                throw new InvalidFormatGroupNameException(input);
            new_group._edu_type.SetNumber(new_group, (Edu)(input[1] - '0'));

            if (input[2] is < '0' or > '9')
                throw new InvalidFormatGroupNameException(input);
            new_group._course = new CourseNumber(new_group, input[2] - '0');

            success = int.TryParse(input.AsSpan(3, 2), out x);
            if (!success)
                throw new InvalidFormatGroupNameException(input);
            new_group._number.SetNumber(new_group, x);

            if (input.Length == 6)
            {
                if (input[5] is < '0' or > '9')
                    throw new InvalidFormatGroupNameException(input);
                new_group._spec.SetNumber(new_group, input[5] - '0');
            }
            else
            {
                new_group._spec.SetNumber(new_group, NoneSpec);
            }

            return new_group;
        }
        else
        {
            throw new InvalidFormatGroupNameException(input);
        }
    }

    public override string ToString() => _letter.Letter switch
    {
        ' ' => $"{(int)_edu_type.Number}{_number.Number:D3}",
        _ => $"{_letter.Letter}{(int)_edu_type.Number}{_course.Number}{_number.Number:D2}{(_spec.Number == NoneSpec ? null : _spec.Number)}"
    };
}