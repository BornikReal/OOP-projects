using Isu.Extra.Exception;
using Isu.Extra.Models;

namespace Isu.Extra.ExtraStudy;

public class ExtraCourse
{
    private readonly List<ExtraStream> _streams = new List<ExtraStream>();

    public ExtraCourse(string courseName, Megafacultet megafacultet)
    {
        CourseName = courseName;
        Megafacultet = megafacultet;
    }

    public string CourseName { get; }
    public Megafacultet Megafacultet { get; }
    public IReadOnlyList<ExtraStream> Streams => _streams;

    public ExtraStream AddNewStream(string streamName, int maxSize, Schedule lessons)
    {
        if (_streams.Find(s => s.StreamName == streamName) != null)
            throw new CGTAAlreadyExistException(streamName);
        var newStream = new ExtraStream(streamName, maxSize, lessons, this);
        _streams.Add(newStream);
        return newStream;
    }
}
