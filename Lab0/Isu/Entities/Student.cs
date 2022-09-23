using Isu.Models;

namespace Isu.Entities;
public class Student
{
    private readonly GeneratorId _generatorId;
    public Student(string name, Group group)
    {
        Name = name;
        _generatorId = new GeneratorId();
        Id = _generatorId.Generate();
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