using Isu.Exception;

namespace Isu.Models;

public class GroupName
{
    private const int MaxGroupNumBMS = 99;
    private const int MaxGroupNumPD = 999;

    private const int MaxMagCourse = 2;
    private const int MaxBachCourse = 5;
    private const int MaxSpecCourse = 6;
    private const int MaxPDCourse = 9;

    private const char PDLetter = ' ';
    private const int NoneSpec = 0;

    private const int PDGroupNameLen = 4;
    private const int BMSGroupNameLenNoSpec = 5;
    private const int BMSGroupNameLen = 6;

    private char _type;
    private Edu _edu_type;
    private CourseNumber _course;
    private int _number;
    private int _spec;

    public GroupName(string name)
    {
        GroupName new_group = Parse(name);
        (_type, _edu_type, _course, _number, _spec) = (new_group._type, new_group._edu_type, new_group._course, new_group._number, new_group._spec);
    }

    public GroupName(char type = 'M', Edu edu_type = Edu.BachId, int number = 1, int spec = 0, int course = 1)
    {
        ValidateInput(type, edu_type, number, spec, course);
        (_type, _edu_type, _number, _spec) = (type, edu_type, number, spec);
        _course = new CourseNumber
        {
            Number = course,
        };
    }

    public GroupName(CourseNumber course, char type = 'M', Edu edu_type = Edu.BachId, int number = 1, int spec = 0)
    {
        ValidateInput(type, edu_type, number, spec, course.Number);
        (_type, _edu_type, _number, _spec, _course) = (type, edu_type, number, spec, course);
    }

    public enum Edu
    {
        BachId = 3,
        MagId = 4,
        SpecId = 5,
        PostGradId = 7,
        DoctId = 8,
    }

    public char Type
    {
        get => _type;
        set
        {
            _type = value;
            ValidateInput(Type, EduType, Number, Spec, Course.Number);
        }
    }

    public Edu EduType
        {
        get => _edu_type;
        set
        {
            _edu_type = value;
            ValidateInput(Type, EduType, Number, Spec, Course.Number);
        }
    }

    public CourseNumber Course
    {
        get => _course;
        set
        {
            _course = value;
            ValidateInput(Type, EduType, Number, Spec, Course.Number);
        }
    }

    public int Number
    {
        get => _number;
        set
        {
            _number = value;
            ValidateInput(Type, EduType, Number, Spec, Course.Number);
        }
    }

    public int Spec
    {
        get => _spec;
        set
        {
            _spec = value;
            ValidateInput(Type, EduType, Number, Spec, Course.Number);
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
            new_group._edu_type = (Edu)(input[0] - '0');

            success = int.TryParse(input[1..], out x);
            if (!success)
                throw new InvalidFormatGroupNameException(input);
            new_group._number = x;

            new_group._spec = NoneSpec;
            new_group._course.Number = 7;

            ValidateInput(new_group.Type, new_group.EduType, new_group.Number, new_group.Spec, new_group.Course.Number);
            return new_group;
        }
        else if (input.Length is BMSGroupNameLenNoSpec or BMSGroupNameLen)
        {
            new_group._type = input[0];

            if (input[1] is not (char)(Edu.BachId + '0') and not (char)(Edu.SpecId + '0') and not (char)(Edu.MagId + '0'))
                throw new InvalidFormatGroupNameException(input);
            new_group._edu_type = (Edu)(input[1] - '0');

            if (input[2] is < '0' or > '9')
                throw new InvalidFormatGroupNameException(input);
            new_group._course = new CourseNumber(input[2] - '0');

            success = int.TryParse(input.AsSpan(3, 2), out x);
            if (!success)
                throw new InvalidFormatGroupNameException(input);
            new_group._number = x;

            if (input.Length == 6)
            {
                if (input[5] is < '0' or > '9')
                    throw new InvalidFormatGroupNameException(input);
                new_group._spec = input[5] - '0';
            }
            else
            {
                new_group._spec = NoneSpec;
            }

            ValidateInput(new_group.Type, new_group.EduType, new_group.Number, new_group.Spec, new_group.Course.Number);
            return new_group;
        }
        else
        {
            throw new InvalidFormatGroupNameException(input);
        }
    }

    public override string ToString() => Type switch
    {
        ' ' => $"{(int)EduType}{Number:D3}",
        _ => $"{Type}{(int)EduType}{Course.Number}{Number:D2}{(Spec == NoneSpec ? null : Spec)}"
    };

    private static void ValidateInput(char type, Edu edu_type, int number, int spec, int course)
    {
        if (type is(< 'A' or > 'Z') and not ' ')
            throw new FrongGroupInfoException(nameof(type));
        if (type == PDLetter && edu_type != Edu.PostGradId && edu_type != Edu.DoctId)
            throw new FrongGroupInfoException(nameof(edu_type));
        if (type != PDLetter && (edu_type < Edu.BachId || edu_type > Edu.SpecId))
            throw new FrongGroupInfoException(nameof(edu_type));
        if (type != PDLetter && (number < 0 || number > MaxGroupNumBMS))
            throw new FrongGroupInfoException(nameof(number));
        if (type == PDLetter && (number < 0 || number > MaxGroupNumPD))
            throw new FrongGroupInfoException(nameof(number));
        if (spec < 0 || spec > 9)
            throw new FrongGroupInfoException(nameof(spec));
        if ((edu_type == Edu.PostGradId || edu_type == Edu.DoctId) && spec != NoneSpec)
            throw new FrongGroupInfoException(nameof(spec));
        if (edu_type == Edu.MagId && (course < 1 || course > MaxMagCourse))
            throw new FrongGroupInfoException(nameof(course));
        if (edu_type == Edu.BachId && (course < 1 || course > MaxBachCourse))
            throw new FrongGroupInfoException(nameof(course));
        if (edu_type == Edu.SpecId && (course < 1 || course > MaxSpecCourse))
            throw new FrongGroupInfoException(nameof(course));
        if ((edu_type == Edu.PostGradId || edu_type == Edu.DoctId) && (course < 1 || course > MaxPDCourse))
            throw new FrongGroupInfoException(nameof(course));
    }
}