using System.Text.RegularExpressions;
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
        string groupPDRegex = @"^[78]\d{3}$";
        string groupBMSRegex = @"^[A-Z]\d{4}\d?$";
        string groupBMSSpecRegex = @"^[A-Z]\d{5}$";

        var new_group = new GroupName();

        if (Regex.IsMatch(input, groupPDRegex, RegexOptions.Compiled))
        {
            new_group.Letter.SetLetter(PDLetter);
            new_group.EduType.SetNumber(new_group, (Edu)(input[0] - '0'));
            new_group.Course.SetCourse(new_group, 7);
            new_group.Number.SetNumber(new_group, int.Parse(input[1..]));
            new_group.Spec.SetNumber(new_group, NoneSpec);
            return new_group;
        }
        else if (Regex.IsMatch(input, groupBMSRegex, RegexOptions.Compiled))
        {
            new_group.Letter.SetLetter(input[0]);
            new_group.EduType.SetNumber(new_group, (Edu)(input[1] - '0'));
            new_group.Course = new CourseNumber(new_group, input[2] - '0');
            new_group.Number.SetNumber(new_group, int.Parse(input.AsSpan(3, 2)));

            if (Regex.IsMatch(input, groupBMSSpecRegex, RegexOptions.Compiled))
            {
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