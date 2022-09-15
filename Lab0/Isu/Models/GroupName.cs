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

    private char _type;
    private EduTypeNumber _edu_type;
    private CourseNumber _course;
    private GroupNumber _number;
    private SpecNumber _spec;

    public GroupName(string name)
    {
        GroupName new_group = Parse(name);
        (_type, _edu_type, _course, _number, _spec) = (new_group._type, new_group._edu_type, new_group._course, new_group._number, new_group._spec);
    }

    public GroupName(char type, EduTypeNumber edu_type, CourseNumber course, GroupNumber number, SpecNumber spec)
    {
        ValidateInput(type);
        (_type, _edu_type, _number, _spec, _course) = (type, edu_type, number, spec, course);
    }

    private GroupName()
    {
        _edu_type = new EduTypeNumber(this);
        _course = new CourseNumber(this);
        _number = new GroupNumber(this);
        _spec = new SpecNumber(this);
    }

    public char Type
    {
        get => _type;
        set
        {
            _type = value;
            ValidateInput(Type);
        }
    }

    public EduTypeNumber EduType
        {
        get => _edu_type;
        set
        {
            _edu_type = value;
            ValidateInput(Type);
        }
    }

    public CourseNumber Course
    {
        get => _course;
        set
        {
            _course = value;
            ValidateInput(Type);
        }
    }

    public GroupNumber Number
    {
        get => _number;
        set
        {
            _number = value;
            ValidateInput(Type);
        }
    }

    public SpecNumber Spec
    {
        get => _spec;
        set
        {
            _spec = value;
            ValidateInput(Type);
        }
    }

    public static GroupName Parse(string input)
    {
        var new_group = new GroupName();
        int x;
        bool success;
        if (input.Length == PDGroupNameLen)
        {
            new_group._type = PDLetter;

            if (input[0] is not (char)(Edu.PostGradId + '0') and not (char)(Edu.DoctId + '0'))
                throw new InvalidFormatGroupNameException(input);
            new_group._edu_type.SetNumber(new_group, (Edu)(input[0] - '0'));

            new_group._course.SetCourse(new_group, 7);

            success = int.TryParse(input[1..], out x);
            if (!success)
                throw new InvalidFormatGroupNameException(input);
            new_group._number.SetNumber(new_group, x);

            new_group._spec.SetNumber(new_group, NoneSpec);

            ValidateInput(new_group.Type);
            return new_group;
        }
        else if (input.Length is BMSGroupNameLenNoSpec or BMSGroupNameLen)
        {
            new_group._type = input[0];

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

            ValidateInput(new_group.Type);
            return new_group;
        }
        else
        {
            throw new InvalidFormatGroupNameException(input);
        }
    }

    public override string ToString() => Type switch
    {
        ' ' => $"{(int)EduType.Number}{Number:D3}",
        _ => $"{Type}{(int)EduType.Number}{Course.Number}{Number:D2}{(Spec.Number == NoneSpec ? null : Spec)}"
    };

    private static void ValidateInput(char type)
    {
        // var groupRegex = new Regex(@"[A-Z]\d{3}[a-z]?$");
        if (type is(< 'A' or > 'Z') and not ' ')
            throw new FrongGroupInfoException(nameof(type));
    }
}