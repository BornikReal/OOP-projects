using Isu.Extra.Models;

namespace Isu.Extra.CGTA;

public class CGTACourse
{
    private readonly List<CGTAStream> _streams = new List<CGTAStream>();

    public CGTACourse(string courseName, Megafacultet megafacultet)
    {
        CourseName = courseName;
        Megafacultet = megafacultet;
    }

    public string CourseName { get; }
    public Megafacultet Megafacultet { get; }
    public IReadOnlyList<CGTAStream> Streams => _streams;

    public CGTAStream AddNewStream(string streamName, int maxSize, Schedule lessons)
    {
        if (_streams.Find(s => s.StreamName == streamName) != null)
            throw new System.Exception();
        var newStream = new CGTAStream(streamName, maxSize, lessons, this);
        _streams.Add(newStream);
        return newStream;
    }
}
