using System.Text.RegularExpressions;
using Isu.Exception.InvalidGroupNameException;
using Isu.Models.GroupNameParts;
using static Isu.Models.GroupNameParts.EduTypeNumber;
using static Isu.Models.GroupNameParts.GroupLetter;
using static Isu.Models.GroupNameParts.SpecNumber;

namespace Isu.Models;

public class GroupName
{
    public const int PostgradDoctGroupNameLen = 4;
    public const int BachMagSpecGroupNameLenNoSpec = 5;
    public const int BachMagSpecGroupNameLen = 6;

    public GroupName(string name)
    {
        GroupName newGroup = Parse(name);
        Letter = newGroup.Letter;
        EduType = newGroup.EduType;
        Course = newGroup.Course;
        Number = newGroup.Number;
        Spec = newGroup.Spec;
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

    public CourseNumber Course { get; }

    public GroupNumber Number { get; }

    public SpecNumber Spec { get; }

    public static GroupName Parse(string input)
    {
        string groupPDRegex = @"^[78]\d{3}$";
        string groupBMSRegex = @"^[A-Z]\d{4}\d?$";
        string groupBMSSpecRegex = @"^[A-Z]\d{5}$";

        var newGroup = new GroupName();

        if (Regex.IsMatch(input, groupPDRegex, RegexOptions.Compiled))
        {
            newGroup.Letter.SetLetter(PostgradDoctLetter);
            newGroup.EduType.SetNumber(newGroup, (Edu)(input[0] - '0'));
            newGroup.Course.SetCourse(newGroup, 7);
            newGroup.Number.SetNumber(newGroup, int.Parse(input[1..]));
            newGroup.Spec.SetNumber(newGroup, NoneSpec);
            return newGroup;
        }
        else if (Regex.IsMatch(input, groupBMSRegex, RegexOptions.Compiled))
        {
            newGroup.Letter.SetLetter(input[0]);
            newGroup.EduType.SetNumber(newGroup, (Edu)(input[1] - '0'));
            newGroup.Course.SetCourse(newGroup, input[2] - '0');
            newGroup.Number.SetNumber(newGroup, int.Parse(input.AsSpan(3, 2)));

            if (Regex.IsMatch(input, groupBMSSpecRegex, RegexOptions.Compiled))
            {
                newGroup.Spec.SetNumber(newGroup, input[5] - '0');
            }
            else
            {
                newGroup.Spec.SetNumber(newGroup, NoneSpec);
            }

            return newGroup;
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

    public bool Equals(GroupName obj)
    {
        return Letter == obj.Letter && EduType == obj.EduType && Course == obj.Course && Number == obj.Number && Spec == obj.Spec;
    }
}