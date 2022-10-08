using Isu.Models.GroupNameParts;

namespace Isu.Exception.InvalidGroupNameException;

public class InvalidEduTypeException : InvalidGroupNameException
{
    public InvalidEduTypeException(EduId number)
        : base($"Education type {number} doesn't match with group letter.")
    { }
}