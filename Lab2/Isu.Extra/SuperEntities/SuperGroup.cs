using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.SuperEntities;

public class SuperGroup
{
    public SuperGroup(Group group, Schedule schedule)
    {
        Group = group;
        Schedule = schedule;
    }

    public Group Group { get; }

    public Schedule Schedule { get; }
}