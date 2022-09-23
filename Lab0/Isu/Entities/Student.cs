namespace Isu.Entities;
public class Student
{
    public Student(string name, Group group, int id)
    {
        Name = name;
        Id = id;
        group.Add(this);
        Group = group;
    }

    public string Name { get; set; }
    public int Id { get; }
    public Group Group { get; private set; }

    public void ChangeGroup(Group newGroup)
    {
        if (Group == newGroup)
            return;
        Group.Remove(this);
        newGroup.Add(this);
        Group = newGroup;
    }
}