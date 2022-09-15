using Isu.Exception.InvalidGroupNameException;

namespace Isu.Models;

public class GroupLetter
{
    private char _letter;

    public GroupLetter(char letter = 'M')
    {
        SetLetter(letter);
    }

    public char Letter { get => _letter; }

    public void SetLetter(char letter)
    {
        if (letter is(< 'A' or > 'Z') and not ' ')
            throw new FrongGroupInfoException(nameof(letter));
        _letter = letter;
    }
}
