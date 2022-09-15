using Isu.Exception.InvalidGroupNameException;
using static Isu.Models.EduTypeNumber;
using static Isu.Models.GroupName;

namespace Isu.Models;

public class SpecNumber
{
    private int _number;

    public SpecNumber(GroupName group_name, int number = NoneSpec)
    {
        SetNumber(group_name, number);
    }

    public int Number { get => _number; }

    public void SetNumber(GroupName group_name, int number)
    {
        if (number < 0 || number > 9)
            throw new FrongGroupInfoException(nameof(number));
        if ((group_name.EduType.Number == Edu.PostGradId || group_name.EduType.Number == Edu.DoctId) && number != NoneSpec)
            throw new FrongGroupInfoException(nameof(number));
        _number = number;
    }
}
