using Isu.Exception.InvalidGroupNameException;
using Isu.Models.GroupNameParts;
using static Isu.Models.GroupNameParts.EduTypeNumber;
using static Isu.Models.GroupNameParts.GroupLetter;
using static Isu.Models.GroupNameParts.SpecNumber;

namespace Isu.Models;

public class GroupName
{
    public const int PDGroupNameLen = 4;
    public const int BMSGroupNameLenNoSpec = 5;
    public const int BMSGroupNameLen = 6;

    public GroupName(string name)
    {
        GroupName new_group = Parse(name);
        (Letter, EduType, Course, Number, Spec) = (new_group.Letter, new_group.EduType, new_group.Course, new_group.Number, new_group.Spec);
    }

    public GroupName(GroupLetter letter, EduTypeNumber edu_type, CourseNumber course, GroupNumber number, SpecNumber spec)
    {
        (Letter, EduType, Number, Spec, Course) = (letter, edu_type, number, spec, course);
    }

    private GroupName()
    {
        Letter = new GroupLetter();
        EduType = new EduTypeNumber(this);
        Course = new CourseNumber(this);
        Number = new GroupNumber(this);
        Spec = new SpecNumber(this);
    }

    public GroupLetter Letter { get; }

    public EduTypeNumber EduType { get; }

    public CourseNumber Course { get; private set; }

    public GroupNumber Number { get; }

    public SpecNumber Spec { get; }

    public static GroupName Parse(string input)
    {
        // var groupRegex = new Regex(@"[A-Z]\d{3}[a-z]?$");
        var new_group = new GroupName();
        int x;
        bool success;
        if (input.Length == PDGroupNameLen)
        {
            new_group.Letter.SetLetter(PDLetter);

            if (input[0] is not (char)(Edu.PostGradId + '0') and not (char)(Edu.DoctId + '0'))
                throw new InvalidFormatGroupNameException(input);
            new_group.EduType.SetNumber(new_group, (Edu)(input[0] - '0'));

            new_group.Course.SetCourse(new_group, 7);

            success = int.TryParse(input[1..], out x);
            if (!success)
                throw new InvalidFormatGroupNameException(input);
            new_group.Number.SetNumber(new_group, x);

            new_group.Spec.SetNumber(new_group, NoneSpec);
            return new_group;
        }
        else if (input.Length is BMSGroupNameLenNoSpec or BMSGroupNameLen)
        {
            new_group.Letter.SetLetter(input[0]);

            if (input[1] is not (char)(Edu.BachId + '0') and not (char)(Edu.SpecId + '0') and not (char)(Edu.MagId + '0'))
                throw new InvalidFormatGroupNameException(input);
            new_group.EduType.SetNumber(new_group, (Edu)(input[1] - '0'));

            if (input[2] is < '0' or > '9')
                throw new InvalidFormatGroupNameException(input);
            new_group.Course = new CourseNumber(new_group, input[2] - '0');

            success = int.TryParse(input.AsSpan(3, 2), out x);
            if (!success)
                throw new InvalidFormatGroupNameException(input);
            new_group.Number.SetNumber(new_group, x);

            if (input.Length == 6)
            {
                if (input[5] is < '0' or > '9')
                    throw new InvalidFormatGroupNameException(input);
                new_group.Spec.SetNumber(new_group, input[5] - '0');
            }
            else
            {
                new_group.Spec.SetNumber(new_group, NoneSpec);
            }

            return new_group;
        }
        else
        {
            throw new InvalidFormatGroupNameException(input);
        }
    }

    public override string ToString() => Letter.Letter switch
    {
        ' ' => $"{(int)EduType.Number}{Number.Number:D3}",
        _ => $"{Letter.Letter}{(int)EduType.Number}{Course.Number}{Number.Number:D2}{(Spec.Number == NoneSpec ? null : Spec.Number)}"
    };
}