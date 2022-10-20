namespace Isu.Extra.CGTA;

public class CGTACourse
{
    public CGTACourse(string megafacultet, IReadOnlyList<CGTAStream> streams)
    {
        Megafacultet = megafacultet;
        Streams = streams;
    }

    public string Megafacultet { get; }
    public IReadOnlyList<CGTAStream> Streams { get; }
}
