using static Isu.Models.GroupNameParts.EduTypeNumber;

namespace Isu.Exception.InvalidGroupNameException;

public class InvalidEduTypeException : InvalidGroupNameException
{
    public InvalidEduTypeException(Edu number)
        : base($"Education type {number} doesn't match with group letter.")
    { }
}
