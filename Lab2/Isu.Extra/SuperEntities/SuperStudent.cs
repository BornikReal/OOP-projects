using Isu.Entities;
using Isu.Extra.Exception;
using Isu.Extra.ExtraStudy;
using Isu.Extra.Models;

namespace Isu.Extra.SuperEntities;

public class SuperStudent
{
    private readonly List<ExtraStream> _extraStream = new List<ExtraStream>();

    public SuperStudent(Student student, SuperGroup superGroup, int maxStreams = 2)
    {
        Student = student;
        SuperGroup = superGroup;
        MaxStreams = maxStreams;
    }

    public Student Student { get; }
    public SuperGroup SuperGroup { get; }
    public int MaxStreams { get; }

    public void UnsiscribeCGTA(ExtraStream stream)
    {
        ExtraStream? remov = _extraStream.Find(s => s == stream);
        if (remov == null)
            throw new CGTAStudentException(Student.Name);
        remov.RemoveStudent(this);
        _extraStream.Remove(remov);
    }

    public void SuscribeCGTA(ExtraStream stream)
    {
        if (_extraStream.Count == MaxStreams || _extraStream.Any(s => !ValidateCGTA(stream, s)) || Schedule.HaveIntersection(stream.Lessons, SuperGroup.Schedule))
            throw new CGTAStudentException(Student.Name);
        _extraStream.Add(stream);
    }

    private static bool ValidateCGTA(ExtraStream extra1, ExtraStream extra2)
    {
        if (extra2!.Course.Megafacultet != extra1!.Course.Megafacultet || extra2.Course == extra1.Course || Schedule.HaveIntersection(extra1.Lessons, extra2.Lessons))
            return false;
        return true;
    }
}
