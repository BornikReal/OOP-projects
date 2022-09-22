using Isu.Exception.InvalidGroupNameException;
using static Isu.Models.GroupNameParts.GroupLetter;

namespace Isu.Models.GroupNameParts;

public class GroupNumber
{
    public const int MaxGroupNumBachMagSpec = 99;
    public const int MaxGroupNumPostgradDoct = 999;

    public GroupNumber(GroupName groupName, int number = 1)
    {
        SetNumber(groupName, number);
    }

    public int Number { get; private set; }

    public void SetNumber(GroupName groupName, int number)
    {
        if (groupName.Letter.Letter != PostgradDoctLetter && (number < 0 || number > MaxGroupNumBachMagSpec))
            throw new InvalidGroupNumberException(number);
        if (groupName.Letter.Letter == PostgradDoctLetter && (number < 0 || number > MaxGroupNumPostgradDoct))
            throw new InvalidGroupNumberException(number);
        Number = number;
    }

    public bool Equals(GroupNumber obj)
    {
        return Number == obj.Number;
    }
}
