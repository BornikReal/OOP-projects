using Isu.Exception.InvalidGroupNameException;
using static Isu.Models.EduTypeNumber;

namespace Isu.Models;

public class SpecNumber
{
    public const int NoneSpec = 0;

    public SpecNumber(GroupName group_name, int number = NoneSpec)
    {
        SetNumber(group_name, number);
    }

    public int Number { get; private set; }

    public void SetNumber(GroupName group_name, int number)
    {
        if (number < 0 || number > 9)
            throw new FrongGroupInfoException(nameof(number));
        if ((group_name.EduType.Number == Edu.PostGradId || group_name.EduType.Number == Edu.DoctId) && number != NoneSpec)
            throw new FrongGroupInfoException(nameof(number));
        Number = number;
    }
}
