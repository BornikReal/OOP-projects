using System.Text.RegularExpressions;
using Isu.Exception.InvalidGroupNameException;
using Isu.Models.GroupNameParts;
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

    private GroupName(char letter, EduId eduId, int course, int number, int spec)
    {
        Letter = new GroupLetter(letter);
        EduType = new EduTypeNumber(this, eduId);
        Course = new CourseNumber(this, course);
        Number = new GroupNumber(this, number);
        Spec = new SpecNumber(this, spec);
    }

    public GroupLetter Letter { get; private set; }

    public EduTypeNumber EduType { get; private set; }

    public CourseNumber Course { get; private set; }

    public GroupNumber Number { get; private set; }

    public SpecNumber Spec { get; private set; }

    public static GroupName Parse(string input)
    {
        string groupPDPattern = @"^[78]\d{3}$";
        string groupBMSPattern = @"^[A-Z]\d{4}\d?$";
        string groupBMSSpecPattern = @"^[A-Z]\d{5}$";

        Match groupPDRegex = Regex.Match(input, groupPDPattern, RegexOptions.Compiled);
        Match groupBMSRegex = Regex.Match(input, groupBMSPattern, RegexOptions.Compiled);

        if (groupPDRegex.Success)
        {
            return new GroupName(PostgradDoctLetter, (EduId)(input[0] - '0'), 7, int.Parse(input[1..]), NoneSpec);
        }
        else if (groupBMSRegex.Success)
        {
            Match groupBMSSpecRegex = Regex.Match(input, groupBMSSpecPattern, RegexOptions.Compiled);
            return new GroupName(input[0], (EduId)(input[1] - '0'), input[2] - '0', int.Parse(input.AsSpan(3, 2)), groupBMSSpecRegex.Success ? input[5] - '0' : NoneSpec);
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