using Isu.Exception.InvalidGroupNameException;
using static Isu.Models.GroupNameParts.EduTypeNumber;

namespace Isu.Models.GroupNameParts;

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
        if (number is < 0 or > 9)
            throw new InvalidSpecNumberException(number);
        if ((group_name.EduType.Number == Edu.PostGradId || group_name.EduType.Number == Edu.DoctId) && number != NoneSpec)
            throw new InvalidSpecNumberException(number);
        Number = number;
    }
}