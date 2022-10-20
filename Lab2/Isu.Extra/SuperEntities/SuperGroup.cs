using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.SuperEntities;

public class SuperGroup
{
    public SuperGroup(Group group, GroupSchedule schedule)
    {
        Group = group;
        Schedule = schedule;
    }

    public Group Group { get; }

    public GroupSchedule Schedule { get; }
}