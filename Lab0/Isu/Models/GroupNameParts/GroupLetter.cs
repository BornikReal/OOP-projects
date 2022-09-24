using Isu.Exception.InvalidGroupNameException;

namespace Isu.Models.GroupNameParts;

public class GroupLetter
{
    public const char PostgradDoctLetter = ' ';

    public GroupLetter(char letter = 'M')
    {
        if (letter is(< 'A' or > 'Z') and not ' ')
            throw new InvalidGroupLetterException(letter);
        Letter = letter;
    }

    public char Letter { get; }

    public bool Equals(GroupLetter obj)
    {
        return Letter == obj.Letter;
    }
}
