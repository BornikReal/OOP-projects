namespace Isu.Extra.Lessons;

public class LessonLocation
{
    public LessonLocation(string nameOfUniversityBuilding, string number)
    {
        NameOfUniversityBuilding = nameOfUniversityBuilding;
        Number = number;
    }

    public string NameOfUniversityBuilding { get; }
    public string Number { get; }
}
