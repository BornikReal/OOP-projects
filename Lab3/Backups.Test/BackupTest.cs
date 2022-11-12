using Backups.Algorithms;
using Backups.Archiver;
using Backups.Models;
using Backups.Repository;
using Xunit;

namespace Backups.Test;

public class BackupTest
{
    [Fact]
    public void UnitTest()
    {
        var repo = new InMemoryRepository("repo");
        IBackupTask elonTask = repo.CreateBackupTask(new SplitStorageAlgorithm(), new ZipArchiver());
        repo.CreateDirectory("test1");
        repo.CreateFile(@"test1\test2.txt").Close();
        repo.CreateFile("test3.txt").Close();
        var backupObjects = new List<BackupObject>() { new BackupObject("test1", repo), new BackupObject("test3.txt", repo) };
        elonTask.AddNewTask(backupObjects[0]);
        elonTask.AddNewTask(backupObjects[1]);
        Assert.Equal(2, repo.OpenDirectory(elonTask.Start()).Entities().Count());
        elonTask.RemoveTask(backupObjects[1]);
        Assert.Single(repo.OpenDirectory(elonTask.Start()).Entities());
    }
}
