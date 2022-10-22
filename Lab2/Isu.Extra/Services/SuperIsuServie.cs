using Isu.Entities;
using Isu.Extra.Exception;
using Isu.Extra.ExtraStudy;
using Isu.Extra.Models;
using Isu.Extra.SuperEntities;
using Isu.Models.GroupNameParts;
using Isu.Services;

namespace Isu.Extra.Services;

public class SuperIsuServie : ISuperIsuServie
{
    private readonly Dictionary<Group, SuperGroup> _groupTranslator = new Dictionary<Group, SuperGroup>();
    private readonly Dictionary<Student, SuperStudent> _studetnTranslator = new Dictionary<Student, SuperStudent>();
    private readonly List<Megafaculty> _megafaculties = new List<Megafaculty>();

    public SuperIsuServie() { }

    public SuperIsuServie(List<Megafaculty> megafaculties)
    {
        _megafaculties = megafaculties;
    }

    public IsuService Isu { get; } = new IsuService();
    public IReadOnlyList<Megafaculty> Megafaculties => _megafaculties;

    public Megafaculty AddNewMegafaculty(string name, List<GroupLetter> faculties)
    {
        var newMegafaculty = new Megafaculty(name, faculties);
        _megafaculties.Add(newMegafaculty);
        return newMegafaculty;
    }

    public ExtraCourse AddNewExtraCourse(string courseName, Megafaculty megafaculty)
    {
        return megafaculty.AddNewCourse(courseName);
    }

    public void AddScheduleToGroup(Group group, Schedule schedule)
    {
        if (_groupTranslator.ContainsKey(group))
            throw new GroupAlreadyHaveScheduleException(group.Name.ToString());
        _groupTranslator.Add(group, new SuperGroup(group, schedule));
    }

    public void AddStudentToExtraStudy(Student student, ExtraStream cGTA)
    {
        if (!_studetnTranslator.ContainsKey(student))
            _studetnTranslator.Add(student, new SuperStudent(student, _groupTranslator[student.Group]));
        _studetnTranslator[student].SuscribeCGTA(cGTA);
    }

    public void RemoveStudentFromExtraStudy(Student student, ExtraStream cGTA)
    {
        if (!_studetnTranslator.ContainsKey(student))
            throw new CGTAStudentException(student.Name);
        _studetnTranslator[student].UnsiscribeCGTA(cGTA);
    }

    public IReadOnlyList<ExtraStream> GetStreamList(ExtraCourse course)
    {
        return course.Streams;
    }

    public IEnumerable<Student> GetStudentList(ExtraStream stream)
    {
        return stream.Students.Select(s => s.Student);
    }

    public IEnumerable<Student> GetListUnsiscribedStudent()
    {
        return Isu.Students.Where(s => !_studetnTranslator.ContainsKey(s));
    }
}
