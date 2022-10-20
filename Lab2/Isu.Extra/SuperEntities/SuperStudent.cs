using Isu.Entities;
using Isu.Extra.CGTA;

namespace Isu.Extra.SuperEntities;

public class SuperStudent
{
    private CGTAStream? _cGTA1;
    private CGTAStream? _cGTA2;
    public SuperStudent(Student student, CGTAStream cGTA1, CGTAStream cGTA2)
    {
        Student = student;
        _cGTA1 = cGTA1;
        _cGTA2 = cGTA2;
        if (CGTA1!.Course.Megafacultet != CGTA2!.Course.Megafacultet || CGTA1.Course == CGTA2.Course)
            throw new System.Exception();
    }

    public Student Student { get; }

    public CGTAStream? CGTA1
    {
        get => _cGTA1;
        set
        {
            if (_cGTA1 == value)
                return;
            if (CGTA2!.Course.Megafacultet != value!.Course.Megafacultet || CGTA2.Course == value.Course)
                throw new System.Exception();
            _cGTA1!.RemoveStudent(this);
            value!.AddStudent(this);
            _cGTA1 = value;
        }
    }

    public CGTAStream? CGTA2
    {
        get => _cGTA2;
        set
        {
            if (_cGTA2 == value)
                return;
            if (CGTA1!.Course.Megafacultet != value!.Course.Megafacultet || CGTA1.Course == value.Course)
                throw new System.Exception();
            _cGTA2!.RemoveStudent(this);
            value!.AddStudent(this);
            _cGTA2 = value;
        }
    }
}
