using Isu.Entities;
using Isu.Extra.CGTA;
using Isu.Extra.Models;
using Isu.Extra.SuperEntities;
using Isu.Models.GroupNameParts;
using Isu.Services;

namespace Isu.Extra.Services;

public class SuperIsuServie : ISuperIsuServie
{
    private readonly Dictionary<Group, SuperGroup> _groupTranslator = new Dictionary<Group, SuperGroup>();
    private readonly Dictionary<Student, SuperStudent> _studetnTranslator = new Dictionary<Student, SuperStudent>();
    private readonly List<Megafacultet> _megafacultets = new List<Megafacultet>();

    public SuperIsuServie() { }

    public SuperIsuServie(List<Megafacultet> megafacultets)
    {
        _megafacultets = megafacultets;
    }

    public IsuService IsuService => new IsuService();
    public IReadOnlyList<Megafacultet> Megafacultets => _megafacultets;

    public Megafacultet AddNewMegafaculty(string name, List<GroupLetter> faculties)
    {
        var newMegafaculty = new Megafacultet(name, faculties);
        _megafacultets.Add(newMegafaculty);
        return newMegafaculty;
    }

    public CGTACourse AddNewCGTACourse(string courseName, Megafacultet megafacultet)
    {
        if (_megafacultets.Find(s => s == megafacultet) == null)
            throw new System.Exception();
        return megafacultet.AddNewCourse(courseName);
    }

    public void AddScheduleToGroup(Group group, Schedule schedule)
    {
        if (_groupTranslator.ContainsKey(group))
            throw new System.Exception();
        _groupTranslator.Add(group, new SuperGroup(group, schedule));
    }

    public void AddStudentToCGTA(Student student, CGTAStream cGTA)
    {
        if (_studetnTranslator.ContainsKey(student))
            _studetnTranslator.Add(student, new SuperStudent(student));
        if (Schedule.HaveIntersection(_groupTranslator[student.Group].Schedule, cGTA.Lessons))
            throw new System.Exception();
        _studetnTranslator[student].SuscribeCGTA(cGTA);
    }

    public void RemoveStudentFromCGTA(Student student, CGTAStream cGTA)
    {
        if (!_studetnTranslator.ContainsKey(student))
            throw new System.Exception();
        _studetnTranslator[student].UnsiscribeCGTA(cGTA);
    }

    public IReadOnlyList<CGTAStream> GetStreamList(CGTACourse course)
    {
        return course.Streams;
    }

    public IEnumerable<Student> GetStudentList(CGTAStream stream)
    {
        return stream.Students.Select(s => s.Student);
    }

    public IEnumerable<Student> GetListUnsiscribedStudent()
    {
        return IsuService.Students.Where(s => !_studetnTranslator.ContainsKey(s));
    }
}
