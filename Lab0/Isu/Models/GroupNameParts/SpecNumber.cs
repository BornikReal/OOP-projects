using Isu.Exception.InvalidGroupNameException;

namespace Isu.Models.GroupNameParts;

public class SpecNumber
{
    public const int NoneSpec = 0;

    public SpecNumber(EduTypeNumber eduNumber, int number = NoneSpec)
    {
        if (number is < 0 or > 9)
            throw new InvalidSpecNumberException(number);
        if ((eduNumber.Number == EduId.PostGradId || eduNumber.Number == EduId.DoctId) && number != NoneSpec)
            throw new InvalidSpecNumberException(number);
        Number = number;
    }

    public int Number { get; }

    public bool Equals(SpecNumber obj)
    {
        return Number == obj.Number;
    }
}