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
        var regexPD = new Regex(@"^([78])(\d{3})$", RegexOptions.Compiled);
        var regexBMS = new Regex(@"^([A-Z])(\d)(\d)(\d{2})(\d?)$", RegexOptions.Compiled);

        Match matchPD = regexPD.Match(input);
        Match matchBMS = regexBMS.Match(input);

        if (matchPD.Success)
        {
            return new GroupName(
                PostgradDoctLetter,
                (EduId)int.Parse(matchPD.Groups[1].Value),
                7,
                int.Parse(matchPD.Groups[2].Value),
                NoneSpec);
        }
        else if (matchBMS.Success)
        {
            return new GroupName(
                char.Parse(matchBMS.Groups[1].Value),
                (EduId)int.Parse(matchBMS.Groups[2].Value),
                int.Parse(matchBMS.Groups[3].Value),
                int.Parse(matchBMS.Groups[4].Value),
                matchBMS.Groups[5].Value.Length != 0 ? int.Parse(matchBMS.Groups[5].Value) : NoneSpec);
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