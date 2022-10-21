using Isu.Entities;
using Isu.Extra.CGTA;
using Isu.Extra.Models;
using Isu.Models.GroupNameParts;

namespace Isu.Extra.Services;

public interface ISuperIsuServie
{
    Megafacultet AddNewMegafaculty(string name, List<GroupLetter> faculties);
    CGTACourse AddNewCGTACourse(string courseName, Megafacultet megafacultet);
    void AddScheduleToGroup(Group group, Schedule schedule);
    void AddStudentToCGTA(Student student, CGTAStream cGTA);
    void RemoveStudentFromCGTA(Student student, CGTAStream cGTA);
    IReadOnlyList<CGTAStream> GetStreamList(CGTACourse course);
    IEnumerable<Student> GetStudentList(CGTAStream stream);
    IEnumerable<Student> GetListUnsiscribedStudent();
}
