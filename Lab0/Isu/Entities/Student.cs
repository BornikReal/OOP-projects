namespace Isu.Entities;
public class Student : IEquatable<Student>
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

    public override bool Equals(object? obj) => Equals(obj as Student);
    public bool Equals(Student? other)
    {
        if (this == other) return true;
        if (other == null) return false;
        if (Name != other.Name) return false;
        if (Id != other.Id) return false;
        if (Group != other.Group) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Id.GetHashCode();
    }
}