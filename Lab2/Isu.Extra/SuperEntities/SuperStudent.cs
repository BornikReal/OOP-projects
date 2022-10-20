using Isu.Entities;

namespace Isu.Extra.SuperEntities;

public class SuperStudent
{
    public SuperStudent(Student student)
    {
        Student = student;
    }

    public Student Student { get; }
}
