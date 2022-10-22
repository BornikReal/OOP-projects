﻿using Isu.Entities;
using Isu.Extra.ExtraStudy;
using Isu.Extra.Models;
using Isu.Models.GroupNameParts;

namespace Isu.Extra.Services;

public interface ISuperIsuServie
{
    Megafacultet AddNewMegafaculty(string name, List<GroupLetter> faculties);
    ExtraCourse AddNewExtraCourse(string courseName, Megafacultet megafacultet);
    void AddScheduleToGroup(Group group, Schedule schedule);
    void AddStudentToExtraStudy(Student student, ExtraStream cGTA);
    void RemoveStudentFromExtraStudy(Student student, ExtraStream cGTA);
    IReadOnlyList<ExtraStream> GetStreamList(ExtraCourse course);
    IEnumerable<Student> GetStudentList(ExtraStream stream);
    IEnumerable<Student> GetListUnsiscribedStudent();
}
