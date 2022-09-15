using Isu.Exception.InvalidGroupNameException;
using static Isu.Models.GroupNameParts.GroupLetter;

namespace Isu.Models.GroupNameParts;

public class GroupNumber
{
    public const int MaxGroupNumBMS = 99;
    public const int MaxGroupNumPD = 999;

    public GroupNumber(GroupName group_name, int number = 1)
    {
        SetNumber(group_name, number);
    }

    public int Number { get; private set; }

    public void SetNumber(GroupName group_name, int number)
    {
        if (group_name.Letter.Letter != PDLetter && (number < 0 || number > MaxGroupNumBMS))
            throw new FrongGroupInfoException(nameof(number));
        if (group_name.Letter.Letter == PDLetter && (number < 0 || number > MaxGroupNumPD))
            throw new FrongGroupInfoException(nameof(number));
        Number = number;
    }
}
