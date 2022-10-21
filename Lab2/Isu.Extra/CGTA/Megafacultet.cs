using Isu.Models.GroupNameParts;

namespace Isu.Extra.CGTA;

public class Megafacultet
{
    private readonly List<GroupLetter> _faculties;
    private readonly List<CGTACourse> _courses = new List<CGTACourse>();

    public Megafacultet(string name, List<GroupLetter> faculties)
    {
        Name = name;
        _faculties = faculties;
    }

    public string Name { get; }

    public bool AllowedFaculty(GroupLetter faculty)
    {
        return _faculties.Contains(faculty);
    }

    public CGTACourse AddNewCourse(string courseName)
    {
        if (_courses.Find(s => s.CourseName == courseName) != null)
            throw new System.Exception();
        var newCousrse = new CGTACourse(courseName, this);
        _courses.Add(newCousrse);
        return newCousrse;
    }
}
