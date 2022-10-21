using Isu.Extra.Builders;
using Isu.Extra.Exception;
using Isu.Extra.Models.LessonParts;
using Isu.Extra.Services;
using Isu.Models.GroupNameParts;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraTest
{
    private readonly SuperIsuServie superIsuServie;
    private readonly CertainLessonBuilder certainLessonBuilder;
    private readonly ScheduleBuilder scheduleBuilder;

    public IsuExtraTest()
    {
        superIsuServie = new SuperIsuServie();
        certainLessonBuilder = new CertainLessonBuilder();
        scheduleBuilder = new ScheduleBuilder();
        superIsuServie.AddNewMegafaculty("TINT", new List<GroupLetter> { new GroupLetter('M') });
        superIsuServie.AddNewMegafaculty("NonameMegafaculty", new List<GroupLetter> { new GroupLetter('Z') });
        superIsuServie.AddNewCGTACourse("CyberBebra", superIsuServie.Megafacultets[0]);
        superIsuServie.AddNewCGTACourse("CyberBebra 2: Uprising", superIsuServie.Megafacultets[0]);
        superIsuServie.AddNewCGTACourse("CyberBebra 3: i am always come back", superIsuServie.Megafacultets[0]);
        superIsuServie.AddNewCGTACourse("figna kakayto", superIsuServie.Megafacultets[1]);
        superIsuServie.Isu.AddGroup(new Isu.Models.GroupName("M32011"));
        superIsuServie.Isu.AddStudent(superIsuServie.Isu.Groups[0], "BornikReal");

        certainLessonBuilder.SetLesson(new Lesson("OOP", LessonType.Practice));
        certainLessonBuilder.AddNewInfo(new LessonLocation("Kronva", "325"), true, Weekend.Saturday, "Gregory", new TimeOnly(10, 0), new TimeOnly(11, 30));
        scheduleBuilder.AddNewLesson(certainLessonBuilder.GetCertainLesson());
        superIsuServie.AddScheduleToGroup(superIsuServie.Isu.Groups[0], scheduleBuilder.GetLessons());

        certainLessonBuilder.Reset();
        scheduleBuilder.Reset();
    }

    [Fact]
    public void AddStudentToCGTAOneCourse()
    {
        certainLessonBuilder.SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture));
        certainLessonBuilder.AddNewInfo(new LessonLocation("Haberj", "320"), false, Weekend.Saturday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30));
        scheduleBuilder.AddNewLesson(certainLessonBuilder.GetCertainLesson());
        superIsuServie.Megafacultets[0].Courses[0].AddNewStream("1", 1, scheduleBuilder.GetLessons());

        certainLessonBuilder.Reset();
        scheduleBuilder.Reset();

        certainLessonBuilder.SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture));
        certainLessonBuilder.AddNewInfo(new LessonLocation("Haberj", "320"), false, Weekend.Wednesday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30));
        scheduleBuilder.AddNewLesson(certainLessonBuilder.GetCertainLesson());
        superIsuServie.Megafacultets[0].Courses[0].AddNewStream("2", 1, scheduleBuilder.GetLessons());

        superIsuServie.AddStudentToCGTA(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[0].Courses[0].Streams[0]);
        Assert.ThrowsAny<CGTAStudentException>(() => superIsuServie.AddStudentToCGTA(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[0].Courses[0].Streams[1]));
    }

    [Fact]
    public void AddStudentToCGTADifferentMegafaculy()
    {
        certainLessonBuilder.SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture));
        certainLessonBuilder.AddNewInfo(new LessonLocation("Haberj", "320"), false, Weekend.Saturday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30));
        scheduleBuilder.AddNewLesson(certainLessonBuilder.GetCertainLesson());
        superIsuServie.Megafacultets[0].Courses[0].AddNewStream("1", 1, scheduleBuilder.GetLessons());

        certainLessonBuilder.Reset();
        scheduleBuilder.Reset();

        certainLessonBuilder.SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture));
        certainLessonBuilder.AddNewInfo(new LessonLocation("Haberj", "320"), false, Weekend.Wednesday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30));
        scheduleBuilder.AddNewLesson(certainLessonBuilder.GetCertainLesson());
        superIsuServie.Megafacultets[1].Courses[0].AddNewStream("2", 1, scheduleBuilder.GetLessons());

        superIsuServie.AddStudentToCGTA(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[0].Courses[0].Streams[0]);
        Assert.ThrowsAny<CGTAStudentException>(() => superIsuServie.AddStudentToCGTA(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[1].Courses[0].Streams[0]));
    }

    [Fact]
    public void AddStudentToCGTAIntersectionWithMainSchedule()
    {
        certainLessonBuilder.SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture));
        certainLessonBuilder.AddNewInfo(new LessonLocation("Haberj", "320"), true, Weekend.Saturday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30));
        scheduleBuilder.AddNewLesson(certainLessonBuilder.GetCertainLesson());
        superIsuServie.Megafacultets[0].Courses[0].AddNewStream("1", 1, scheduleBuilder.GetLessons());

        Assert.ThrowsAny<CGTAStudentException>(() => superIsuServie.AddStudentToCGTA(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[0].Courses[0].Streams[0]));
    }

    [Fact]
    public void AddStudentToThreeCGTA()
    {
        certainLessonBuilder.SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture));
        certainLessonBuilder.AddNewInfo(new LessonLocation("Haberj", "320"), false, Weekend.Saturday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30));
        scheduleBuilder.AddNewLesson(certainLessonBuilder.GetCertainLesson());
        superIsuServie.Megafacultets[0].Courses[0].AddNewStream("1", 1, scheduleBuilder.GetLessons());

        certainLessonBuilder.Reset();
        scheduleBuilder.Reset();

        certainLessonBuilder.SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture));
        certainLessonBuilder.AddNewInfo(new LessonLocation("Haberj", "320"), false, Weekend.Wednesday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30));
        scheduleBuilder.AddNewLesson(certainLessonBuilder.GetCertainLesson());
        superIsuServie.Megafacultets[0].Courses[1].AddNewStream("1", 1, scheduleBuilder.GetLessons());

        certainLessonBuilder.Reset();
        scheduleBuilder.Reset();

        certainLessonBuilder.SetLesson(new Lesson("super lection ot kanjelev", LessonType.Lecture));
        certainLessonBuilder.AddNewInfo(new LessonLocation("Haberj", "320"), false, Weekend.Monday, "MegaKanjelev", new TimeOnly(10, 0), new TimeOnly(11, 30));
        scheduleBuilder.AddNewLesson(certainLessonBuilder.GetCertainLesson());
        superIsuServie.Megafacultets[0].Courses[2].AddNewStream("1", 1, scheduleBuilder.GetLessons());

        superIsuServie.AddStudentToCGTA(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[0].Courses[0].Streams[0]);
        superIsuServie.AddStudentToCGTA(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[0].Courses[1].Streams[0]);
        Assert.ThrowsAny<CGTAStudentException>(() => superIsuServie.AddStudentToCGTA(superIsuServie.Isu.Students[0], superIsuServie.Megafacultets[0].Courses[2].Streams[0]));
    }
}
