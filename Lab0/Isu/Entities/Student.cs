using Isu.Models;

namespace Isu.Entities;
public class Student
{
    private Group _group;
    public Student(string name, Group group)
    {
        Name = name;
        Id = GeneratorId.Generate();
        Group = group;
    }

    public string Name { get; set; }
    public int Id { get; }
    public Group Group
    {
        get => _group;
        set => ChangeGroup(value);
    }

    private void ChangeGroup(Group newGroup)
    {
        Group delGroup = _group;
        _group = null;
        delGroup.Remove(this);
        newGroup.Add(this);
        _group = newGroup;
    }
}