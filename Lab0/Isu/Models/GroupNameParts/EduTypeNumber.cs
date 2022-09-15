﻿using Isu.Exception.InvalidGroupNameException;
using static Isu.Models.GroupNameParts.GroupLetter;

namespace Isu.Models.GroupNameParts;

public class EduTypeNumber
{
    public EduTypeNumber(GroupName group_name, Edu number = Edu.BachId)
    {
        SetNumber(group_name, number);
    }

    public enum Edu
    {
        BachId = 3,
        MagId = 4,
        SpecId = 5,
        PostGradId = 7,
        DoctId = 8,
    }

    public Edu Number { get; private set; }

    public void SetNumber(GroupName group_name, Edu number)
    {
        if (group_name.Letter.Letter == PDLetter && number != Edu.PostGradId && number != Edu.DoctId)
            throw new InvalidEduTypeException(number);
        if (group_name.Letter.Letter != PDLetter && (number < Edu.BachId || number > Edu.SpecId))
            throw new InvalidEduTypeException(number);
        Number = number;
    }
}
