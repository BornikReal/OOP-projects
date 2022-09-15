using Isu.Exception.InvalidGroupNameException;

namespace Isu.Models.GroupNameParts;

public class GroupLetter
{
    public const char PDLetter = ' ';

    public GroupLetter(char letter = 'M')
    {
        SetLetter(letter);
    }

    public char Letter { get; private set; }

    public void SetLetter(char letter)
    {
        if (letter is (< 'A' or > 'Z') and not ' ')
            throw new FrongGroupInfoException(nameof(letter));
        Letter = letter;
    }
}
