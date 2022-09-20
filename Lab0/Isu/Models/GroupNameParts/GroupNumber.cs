using Isu.Exception.InvalidGroupNameException;
using static Isu.Models.GroupNameParts.GroupLetter;

namespace Isu.Models.GroupNameParts;

public class GroupNumber
{
    public const int MaxGroupNumBMS = 99;
    public const int MaxGroupNumPD = 999;

    public GroupNumber(GroupName groupName, int number = 1)
    {
        SetNumber(groupName, number);
    }

    public int Number { get; private set; }

    public void SetNumber(GroupName groupName, int number)
    {
        if (groupName.Letter.Letter != PDLetter && (number < 0 || number > MaxGroupNumBMS))
            throw new InvalidGroupNumberException(number);
        if (groupName.Letter.Letter == PDLetter && (number < 0 || number > MaxGroupNumPD))
            throw new InvalidGroupNumberException(number);
        Number = number;
    }

    public bool Equals(GroupNumber obj)
    {
        return Number == obj.Number;
    }
}
