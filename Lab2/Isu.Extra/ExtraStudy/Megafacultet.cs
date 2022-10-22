using Isu.Extra.Exception;
using Isu.Models.GroupNameParts;

namespace Isu.Extra.ExtraStudy;

public class Megafacultet
{
    private readonly List<GroupLetter> _faculties;
    private readonly List<ExtraCourse> _courses = new List<ExtraCourse>();

    public Megafacultet(string name, List<GroupLetter> faculties)
    {
        Name = name;
        _faculties = faculties;
    }

    public string Name { get; }
    public IReadOnlyList<ExtraCourse> Courses => _courses;

    public bool AllowedFaculty(GroupLetter faculty)
    {
        return _faculties.Contains(faculty);
    }

    public ExtraCourse AddNewCourse(string courseName)
    {
        if (_courses.Find(s => s.CourseName == courseName) != null)
            throw new CGTAAlreadyExistException(courseName);
        var newCousrse = new ExtraCourse(courseName, this);
        _courses.Add(newCousrse);
        return newCousrse;
    }
}
