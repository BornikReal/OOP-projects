using Isu.Exception.InvalidGroupNameException;
using static Isu.Models.GroupName;

namespace Isu.Models;

public class GroupNumber
{
    private int _number;

    public GroupNumber(GroupName group_name, int number = 1)
    {
        SetNumber(group_name, number);
    }

    public int Number { get => _number; }

    public void SetNumber(GroupName group_name, int number)
    {
        if (group_name.Letter.Letter != PDLetter && (number < 0 || number > MaxGroupNumBMS))
            throw new FrongGroupInfoException(nameof(number));
        if (group_name.Letter.Letter == PDLetter && (number < 0 || number > MaxGroupNumPD))
            throw new FrongGroupInfoException(nameof(number));
        _number = number;
    }
}
