using Isu.Entities;
using Isu.Exception;
using Isu.Exception.InvalidGroupNameException;
using Xunit;

namespace Isu.Test;

public class IsuService
{
    private readonly Isu.Services.IsuService testIsu;

    public IsuService()
    {
        testIsu = new Isu.Services.IsuService();
    }

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var groupName1 = new Isu.Models.GroupName("M32011");
        var groupName2 = new Isu.Models.GroupName("M32001");
        Group testGroup1 = testIsu.AddGroup(groupName1);
        Group testGroup2 = testIsu.AddGroup(groupName2);
        Student testStudent1 = testIsu.AddStudent(testGroup1, "BornikReal");
        Student testStudent2 = testIsu.AddStudent(testGroup2, "Maksise4ka");
        testIsu.ChangeStudentGroup(testStudent2, testGroup1);

        Assert.Equal(testGroup1, testStudent1.Group);
        Assert.Equal(testGroup1, testStudent2.Group);
        Assert.Equal(2, testGroup1.Students.Count);
        Assert.Empty(testGroup2.Students);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var groupName = new Isu.Models.GroupName("M32011");
        Group testGroup = testIsu.AddGroup(groupName);
        for (int i = 0; i < 30; i++)
            testIsu.AddStudent(testGroup, "BornikReal");
        Assert.ThrowsAny<GroupOverflowException>(() => testIsu.AddStudent(testGroup, "BornikReal"));
    }

    [Theory]
    [InlineData("jakdh")]
    [InlineData("ahsjd")]
    [InlineData("M0101")]
    [InlineData("M9999")]
    public void CreateGroupWithInvalidName_ThrowException(string name)
    {
        Assert.ThrowsAny<InvalidGroupNameException>(() => testIsu.AddGroup(new Models.GroupName(name)));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var groupName1 = new Isu.Models.GroupName("M32011");
        var groupName2 = new Isu.Models.GroupName("M32001");
        Group testGroup1 = testIsu.AddGroup(groupName1);
        Group testGroup2 = testIsu.AddGroup(groupName2);
        Student testStudent = testIsu.AddStudent(testGroup1, "BornikReal");
        testIsu.ChangeStudentGroup(testStudent, testGroup2);

        Assert.Equal(testGroup2, testStudent.Group);
        Assert.Single(testGroup2.Students);
        Assert.Empty(testGroup1.Students);
    }
}