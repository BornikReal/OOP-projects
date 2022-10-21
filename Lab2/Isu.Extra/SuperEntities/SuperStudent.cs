using Isu.Entities;
using Isu.Extra.CGTA;
using Isu.Extra.Exception;

namespace Isu.Extra.SuperEntities;

public class SuperStudent
{
    private readonly List<CGTAStream> _cGTAStreams = new List<CGTAStream>();

    public SuperStudent(Student student, int maxStreams = 2)
    {
        Student = student;
        MaxStreams = maxStreams;
    }

    public Student Student { get; }
    public int MaxStreams { get; }

    public void UnsiscribeCGTA(CGTAStream stream)
    {
        CGTAStream? remov = _cGTAStreams.Find(s => s == stream);
        if (remov == null)
            throw new CGTAStudentException(Student.Name);
        remov.RemoveStudent(this);
        _cGTAStreams.Remove(remov);
    }

    public void SuscribeCGTA(CGTAStream stream)
    {
        if (_cGTAStreams.Count == MaxStreams || _cGTAStreams.Any(s => !ValidateCGTA(stream, s)))
            throw new CGTAStudentException(Student.Name);
        _cGTAStreams.Add(stream);
    }

    private static bool ValidateCGTA(CGTAStream cGTA1, CGTAStream cGTA2)
    {
        if (cGTA2!.Course.Megafacultet != cGTA1!.Course.Megafacultet || cGTA2.Course == cGTA1.Course)
            return false;
        return true;
    }
}
