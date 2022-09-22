using Isu.Exception.InvalidGroupNameException;
using static Isu.Models.GroupNameParts.GroupLetter;

namespace Isu.Models.GroupNameParts;

public class EduTypeNumber
{
    public EduTypeNumber(GroupName groupName, EduId number = EduId.BachId)
    {
        SetNumber(groupName, number);
    }

    public EduId Number { get; private set; }

    public void SetNumber(GroupName groupName, EduId number)
    {
        if (groupName.Letter.Letter == PostgradDoctLetter && number != EduId.PostGradId && number != EduId.DoctId)
            throw new InvalidEduTypeException(number);
        if (groupName.Letter.Letter != PostgradDoctLetter && (number < EduId.BachId || number > EduId.SpecId))
            throw new InvalidEduTypeException(number);
        Number = number;
    }

    public bool Equals(EduTypeNumber obj)
    {
        return Number == obj.Number;
    }
}
