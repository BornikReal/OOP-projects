using Isu.Exception.IdException;
using Isu.Models;

namespace Isu.Entities;
public class Student
{
    private Group? _group;
    public Student(string name, int id = -1, Group? group = null)
    {
        Name = name;
        if (id == -1)
        {
            Id = GeneratorId.Generate();
        }
        else if (id is >= GeneratorId.MinId and <= GeneratorId.MaxId)
        {
            if (!GeneratorId.CheckAvail(id))
                throw new IdOutOfRangeException(id);
            Id = id;
        }
        else
        {
            throw new IdOutOfRangeException(id);
        }

        Group = group;
    }

    public string Name { get; set; }
    public int Id { get; }
    public Group? Group
    {
        get => _group;
        set => ChangeGroup(value);
    }

    private void ChangeGroup(Group? newGroup)
    {
        if (_group != null)
        {
            Group del_group = _group;
            _group = null;
            del_group.Remove(this);
        }

        if (newGroup != null)
            newGroup.Add(this);
        _group = newGroup;
    }
}