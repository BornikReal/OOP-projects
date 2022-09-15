using Isu.Entities;
using Isu.Exception.InvalidGroupNameException;
using Xunit;

namespace Isu.Test;

public class IsuService
{
    private Isu.Services.IsuService testIsu;

    public IsuService()
    {
        testIsu = new Isu.Services.IsuService();
    }

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var group_name1 = new Isu.Models.GroupName("M32011");
        var group_name2 = new Isu.Models.GroupName("M32001");
        Group test_group1 = testIsu.AddGroup(group_name1);
        Group test_group2 = testIsu.AddGroup(group_name2);
        Student test_student1 = testIsu.AddStudent(test_group1, "BornikReal");
        Student test_student2 = testIsu.AddStudent(test_group2, "Maksise4ka");
        testIsu.ChangeStudentGroup(test_student2, test_group1);

        Assert.Equal(test_group1, test_student1.Group);
        Assert.Equal(test_group1, test_student2.Group);
        Assert.Equal(2, test_group1.Students.Count);
        Assert.Empty(test_group2.Students);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException() { }

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
        var group_name1 = new Isu.Models.GroupName("M32011");
        var group_name2 = new Isu.Models.GroupName("M32001");
        Group test_group1 = testIsu.AddGroup(group_name1);
        Group test_group2 = testIsu.AddGroup(group_name2);
        Student test_student = testIsu.AddStudent(test_group1, "BornikReal");
        testIsu.ChangeStudentGroup(test_student, test_group2);

        Assert.Equal(test_group2, test_student.Group);
        Assert.Single(test_group2.Students);
        Assert.Empty(test_group1.Students);
    }
}