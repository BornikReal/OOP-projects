using System.Text;
using Backups.Algorithms;
using Backups.Archiver;
using Backups.FileSystemEntities;
using Backups.FileSystemEntities.Interfaces;
using Backups.Interlayer;
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
        var elonTask = new BackupTask(repo, new SplitStorageAlgorithm(new ZipArchiver()));
        repo.CreateDirectory("test1");
        Stream stream1 = repo.CreateFile(@"test1\test2.txt");
        stream1.Write(Encoding.UTF8.GetBytes("Hello, World!"));
        stream1.Close();
        Stream stream2 = repo.CreateFile("test3.txt");
        stream2.Write(Encoding.UTF8.GetBytes("Goodbye, this f**king world!"));
        stream2.Close();
        var backupObjects = new List<BackupObject>() { new BackupObject("test1", repo), new BackupObject("test3.txt", repo) };

        elonTask.AddNewTask(backupObjects[0]);
        elonTask.AddNewTask(backupObjects[1]);
        RestorePoint restorePoint1 = elonTask.Start();
        Assert.Equal(2, ((IDirectoryEntity)repo.OpenEntity(restorePoint1.RestorePointPath)).Entities().Count());
        IRepoDisposable repDis1 = restorePoint1.Storage.GetEntities();
        var entities1 = repDis1.Entities.ToList();
        Stream stream3 = ((FileEntity)entities1[1]).FuncStream();
        Assert.Equal("Goodbye, this f**king world!", new StreamReader(stream3).ReadToEnd());
        stream3.Close();
        repDis1.Dispose();

        elonTask.RemoveTask(backupObjects[1]);
        RestorePoint restorePoint2 = elonTask.Start();
        Assert.Single(((IDirectoryEntity)repo.OpenEntity(restorePoint2.RestorePointPath)).Entities());
        IRepoDisposable repDis2 = restorePoint2.Storage.GetEntities();
        var entities2 = repDis2.Entities.ToList();
        Stream stream4 = ((FileEntity)((DirectoryEntity)entities2[0]).Entities().ToList()[0]).FuncStream();
        Assert.Equal("Hello, World!", new StreamReader(stream4).ReadToEnd());
        stream4.Close();
        repDis2.Dispose();
    }
}
