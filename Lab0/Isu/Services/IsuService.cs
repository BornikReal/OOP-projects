﻿using Isu.Entities;
using Isu.Exception;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Student> _students;
    private readonly List<Group> _groups;

    public IsuService()
    {
        _students = new List<Student>();
        _groups = new List<Group>();
    }

    public IReadOnlyList<Student> Students => _students;

    public IReadOnlyList<Group> Groups => _groups;

    public Group AddGroup(GroupName name)
    {
        _groups.Add(new Group(name));
        return _groups.Last();
    }

    public Student AddStudent(Group group, string name)
    {
        _students.Add(new Student(name));
        _students.Last().Group = group;
        return _students.Last();
    }

    public Student? FindStudent(int id)
    {
        return _students.Find(s => s.Id == id);
    }

    public Student GetStudent(int id)
    {
        return _students.Find(s => s.Id == id) ?? throw new StudentIdNotFoundException(id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        return _students.FindAll(s => s.Group != null && s.Group.Name == groupName);
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return _students.FindAll(s => s.Group != null && s.Group.Name.Course == courseNumber);
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.Find(g => g.Name == groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups.FindAll(g => g.Name.Course == courseNumber);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        student.Group = newGroup;
    }
}
