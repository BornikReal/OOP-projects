using Isu.Exception.InvalidGroupNameException;
using static Isu.Models.GroupNameParts.GroupLetter;

namespace Isu.Models.GroupNameParts;

public class EduTypeNumber
{
    public EduTypeNumber(GroupLetter letter, EduId number = EduId.BachId)
    {
        if (letter.Letter == PostgradDoctLetter && number != EduId.PostGradId && number != EduId.DoctId)
            throw new InvalidEduTypeException(number);
        if (letter.Letter != PostgradDoctLetter && (number < EduId.BachId || number > EduId.SpecId))
            throw new InvalidEduTypeException(number);
        Number = number;
    }

    public EduId Number { get; }

    public bool Equals(EduTypeNumber obj)
    {
        return Number == obj.Number;
    }
}
