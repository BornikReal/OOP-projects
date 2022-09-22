using Isu.Models;

namespace Isu.Entities;
public class Student
{
    private Group _group;
    public Student(string name, Group group)
    {
        Name = name;
        Id = GeneratorId.Generate();
        group.Add(this);
        _group = group;
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
        if (_group == newGroup)
            return;
        _group.Remove(this);
        newGroup.Add(this);
        _group = newGroup;
    }
}