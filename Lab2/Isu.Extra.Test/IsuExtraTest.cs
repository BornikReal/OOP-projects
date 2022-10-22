using Isu.Extra.Exception;
using Isu.Extra.Models;
using Isu.Extra.Models.LessonParts;
using Isu.Extra.Services;
using Isu.Models.GroupNameParts;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraTest
{
    private readonly SuperIsuServie superIsuServie;

    public IsuExtraTest()
    {
        superIsuServie = new SuperIsuServie();
        superIsuServie.AddNewMegafaculty("TINT", new List<GroupLetter> { new GroupLetter('M') });
        superIsuServie.AddNewMegafaculty("NonameMegafaculty", new List<GroupLetter> { new GroupLetter('Z') });
        superIsuServie.AddNewExtraCourse("CyberBebra", superIsuServie.Megafacultets[0]);
        superIsuServie.AddNewExtraCourse("CyberBebra 2: Uprising", superIsuServie.Megafacultets[0]);
        superIsuServie.AddNewExtraCourse("CyberBebra 3: i am always come back", superIsuServie.Megafacultets[0]);
        superIsuServie.AddNewExtraCourse("figna kakayto", superIsuServie.Megafacultets[1]);
        superIsuServie.Isu.AddGroup(new Isu.Models.GroupName("M32011"));
        superIsuServie.Isu.AddStudent(superIsuServie.Isu.Groups[0], "BornikReal");

        CertainLesson certainLesson = CertainLesson.Builder
            .SetLesson(new Lesson("OOP", LessonType.Practice))
            .AddNewInfo(new LessonLocation("Kronva", "325"), true, Weekend.Saturday, "Gregory", new TimeOnly(10, 0), new TimeOnly(11, 30))
            .Build();
        Schedule schedule = Schedule.Builder
                                    .AddNewLesson(certainLesson)
                                    .Build();
        superIsuServie.AddScheduleToGroup(superIsuServie.Isu.Groups[0], schedule);
    }

    [Fact]
    public void AddStudentToCGTAOneCourse()
    {
        CertainLesson newCertainLesson1 = CertainLesson.Builder
            .SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture))
            .AddNewInfo(new LessonLocation("Haberj", "320"), false, Weekend.Saturday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30))
            .Build();
        Schedule newSchedule1 = Schedule.Builder
                                    .AddNewLesson(newCertainLesson1)
                                    .Build();
        superIsuServie.Megafacultets[0].Courses[0].AddNewStream("1", 1, newSchedule1);

        CertainLesson newCertainLesson2 = CertainLesson.Builder
            .SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture))
            .AddNewInfo(new LessonLocation("Haberj", "320"), false, Weekend.Wednesday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30))
            .Build();
        Schedule newSchedule2 = Schedule.Builder
                                    .AddNewLesson(newCertainLesson1)
                                    .Build();
        superIsuServie.Megafacultets[0].Courses[0].AddNewStream("2", 1, newSchedule2);

        superIsuServie.AddStudentToExtraStudy(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[0].Courses[0].Streams[0]);
        Assert.ThrowsAny<CGTAStudentException>(() => superIsuServie.AddStudentToExtraStudy(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[0].Courses[0].Streams[1]));
    }

    [Fact]
    public void AddStudentToCGTADifferentMegafaculy()
    {
        CertainLesson newCertainLesson1 = CertainLesson.Builder
            .SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture))
            .AddNewInfo(new LessonLocation("Haberj", "320"), false, Weekend.Saturday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30))
            .Build();
        Schedule newSchedule1 = Schedule.Builder
                                    .AddNewLesson(newCertainLesson1)
                                    .Build();
        superIsuServie.Megafacultets[0].Courses[0].AddNewStream("1", 1, newSchedule1);

        CertainLesson newCertainLesson2 = CertainLesson.Builder
            .SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture))
            .AddNewInfo(new LessonLocation("Haberj", "320"), false, Weekend.Wednesday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30))
            .Build();
        Schedule newSchedule2 = Schedule.Builder
                                    .AddNewLesson(newCertainLesson1)
                                    .Build();
        superIsuServie.Megafacultets[1].Courses[0].AddNewStream("2", 1, newSchedule2);

        superIsuServie.AddStudentToExtraStudy(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[0].Courses[0].Streams[0]);
        Assert.ThrowsAny<CGTAStudentException>(() => superIsuServie.AddStudentToExtraStudy(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[1].Courses[0].Streams[0]));
    }

    [Fact]
    public void AddStudentToCGTAIntersectionWithMainSchedule()
    {
        CertainLesson newCertainLesson = CertainLesson.Builder
            .SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture))
            .AddNewInfo(new LessonLocation("Haberj", "320"), true, Weekend.Saturday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30))
            .Build();
        Schedule newSchedule = Schedule.Builder
                                    .AddNewLesson(newCertainLesson)
                                    .Build();
        superIsuServie.Megafacultets[0].Courses[0].AddNewStream("1", 1, newSchedule);

        Assert.ThrowsAny<CGTAStudentException>(() => superIsuServie.AddStudentToExtraStudy(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[0].Courses[0].Streams[0]));
    }

    [Fact]
    public void AddStudentToThreeCGTA()
    {
        CertainLesson newCertainLesson1 = CertainLesson.Builder
           .SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture))
           .AddNewInfo(new LessonLocation("Haberj", "320"), false, Weekend.Saturday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30))
           .Build();
        Schedule newSchedule1 = Schedule.Builder
                                    .AddNewLesson(newCertainLesson1)
                                    .Build();
        superIsuServie.Megafacultets[0].Courses[0].AddNewStream("1", 1, newSchedule1);

        CertainLesson newCertainLesson2 = CertainLesson.Builder
            .SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture))
            .AddNewInfo(new LessonLocation("Haberj", "320"), false, Weekend.Wednesday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30))
            .Build();
        Schedule newSchedule2 = Schedule.Builder
                                    .AddNewLesson(newCertainLesson2)
                                    .Build();
        superIsuServie.Megafacultets[0].Courses[1].AddNewStream("1", 1, newSchedule2);

        CertainLesson newCertainLesson3 = CertainLesson.Builder
            .SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture))
            .AddNewInfo(new LessonLocation("Haberj", "320"), false, Weekend.Monday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30))
            .Build();
        Schedule newSchedule3 = Schedule.Builder
                                    .AddNewLesson(newCertainLesson3)
                                    .Build();
        superIsuServie.Megafacultets[0].Courses[2].AddNewStream("1", 1, newSchedule3);

        superIsuServie.AddStudentToExtraStudy(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[0].Courses[0].Streams[0]);
        superIsuServie.AddStudentToExtraStudy(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[0].Courses[1].Streams[0]);
        Assert.ThrowsAny<CGTAStudentException>(() => superIsuServie.AddStudentToExtraStudy(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[0].Courses[2].Streams[0]));
    }
}
