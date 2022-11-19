using System.Text;
using Backups.Archiver;
using Backups.Extra.Cleaner;
using Backups.Extra.Deleter;
using Backups.Extra.LoggingEntities;
using Backups.Extra.Merger;
using Backups.Extra.Models;
using Backups.Extra.RepositorySuper;
using Backups.Extra.Visitor;
using Backups.FileSystemEntities;
using Backups.FileSystemEntities.Interfaces;
using Backups.Interlayer;
using Backups.Models;
using Backups.Strategy;
using Xunit;

namespace Backups.Extra.Test;

public class ConsoleTestExtra
{
    [Fact]
    public static void TestCompatibility()
    {
        var repository = new InMemoryRepositorySuper("repo");
        var logger = new ConsoleLogger(true);
        var backupTask = new BackupTaskSuper(new NowTimeStrategy(), repository, new SplitStorageAlgorithmVisitor(new ZipArchiver()), logger);
        repository.CreateDirectory("test1");
        Stream stream1 = repository.CreateFile(@"test1\test2.txt");
        stream1.Write(Encoding.UTF8.GetBytes("Hello, World!"));
        stream1.Close();
        Stream stream2 = repository.CreateFile("test3.txt");
        stream2.Write(Encoding.UTF8.GetBytes("Goodbye, this f**king world!"));
        stream2.Close();
        var backupObjects = new List<BackupObject>() { new BackupObject("test1", repository), new BackupObject("test3.txt", repository) };
        backupTask.AddNewTask(backupObjects[0]);
        backupTask.AddNewTask(backupObjects[1]);

        RestorePoint restorePoint1 = backupTask.Start();
        Assert.Equal(2, ((IDirectoryEntity)repository.OpenEntity(restorePoint1.RestorePointPath)).Entities().Count());
        IRepoDisposable repDis1 = restorePoint1.Storage.GetEntities();
        var entities1 = repDis1.Entities.ToList();
        Stream stream3 = ((FileEntity)entities1[1]).FuncStream();
        Assert.Equal("Goodbye, this f**king world!", new StreamReader(stream3).ReadToEnd());
        stream3.Close();
        repDis1.Dispose();

        backupTask.RemoveTask(backupObjects[1]);
        RestorePoint restorePoint2 = backupTask.Start();
        Assert.Single(((IDirectoryEntity)repository.OpenEntity(restorePoint2.RestorePointPath)).Entities());
        IRepoDisposable repDis2 = restorePoint2.Storage.GetEntities();
        var entities2 = repDis2.Entities.ToList();
        Stream stream4 = ((FileEntity)((DirectoryEntity)entities2[0]).Entities().ToList()[0]).FuncStream();
        Assert.Equal("Hello, World!", new StreamReader(stream4).ReadToEnd());
        stream4.Close();
        repDis2.Dispose();
    }

    [Fact]
    public static void TestCleaner()
    {
        var repository = new InMemoryRepositorySuper("repo");
        var logger = new ConsoleLogger(true);
        var backupTask = new BackupTaskSuper(new NowTimeStrategy(), repository, new SplitStorageAlgorithmVisitor(new ZipArchiver()), logger);
        repository.CreateDirectory("test1");
        Stream stream1 = repository.CreateFile(@"test1\test2.txt");
        stream1.Write(Encoding.UTF8.GetBytes("Hello, World!"));
        stream1.Close();
        Stream stream2 = repository.CreateFile("test3.txt");
        stream2.Write(Encoding.UTF8.GetBytes("Goodbye, this f**king world!"));
        stream2.Close();
        var backupObjects = new List<BackupObject>() { new BackupObject("test1", repository), new BackupObject("test3.txt", repository) };
        backupTask.AddNewTask(backupObjects[0]);
        backupTask.AddNewTask(backupObjects[1]);

        backupTask.Start();
        backupTask.RemoveTask(backupObjects[1]);
        backupTask.Start();
        ICleaner cleaner = new NumberCleaner(1);

        backupTask.CleanRestorePoints(cleaner, new RestorePointDeleter(repository));
        Assert.Single(((IDirectoryEntity)repository.OpenEntity(backupTask.BackupTaskPath)).Entities());
    }

    [Fact]
    public static void TestMergerWithSingleAlgorithm()
    {
        var repository = new InMemoryRepositorySuper("repo");
        var logger = new ConsoleLogger(true);
        var backupTask = new BackupTaskSuper(new NowTimeStrategy(), repository, new SingleStorageAlgorithmVisitor(new ZipArchiver()), logger);
        repository.CreateDirectory("test1");
        Stream stream1 = repository.CreateFile(@"test1\test2.txt");
        stream1.Write(Encoding.UTF8.GetBytes("Hello, World!"));
        stream1.Close();
        Stream stream2 = repository.CreateFile("test3.txt");
        stream2.Write(Encoding.UTF8.GetBytes("Goodbye, this f**king world!"));
        stream2.Close();
        var backupObjects = new List<BackupObject>() { new BackupObject("test1", repository), new BackupObject("test3.txt", repository) };
        backupTask.AddNewTask(backupObjects[0]);
        backupTask.AddNewTask(backupObjects[1]);

        backupTask.Start();
        backupTask.RemoveTask(backupObjects[1]);
        backupTask.Start();

        var merger = new RestorePointMerger(new RestorePointDeleter(repository));

        backupTask.Merge(backupTask.RestorePoints, merger);

        Assert.Single(((IDirectoryEntity)repository.OpenEntity(backupTask.BackupTaskPath)).Entities());
    }

    [Fact]
    public static void TestMergerWithSplitAlgorithm()
    {
        var repository = new InMemoryRepositorySuper("repo");
        var logger = new ConsoleLogger(true);
        var backupTask = new BackupTaskSuper(new NowTimeStrategy(), repository, new SplitStorageAlgorithmVisitor(new ZipArchiver()), logger);
        repository.CreateDirectory("test1");
        Stream stream1 = repository.CreateFile(@"test1\test2.txt");
        stream1.Write(Encoding.UTF8.GetBytes("Hello, World!"));
        stream1.Close();
        Stream stream2 = repository.CreateFile("test3.txt");
        stream2.Write(Encoding.UTF8.GetBytes("Goodbye, this f**king world!"));
        stream2.Close();
        var backupObjects = new List<BackupObject>() { new BackupObject("test1", repository), new BackupObject("test3.txt", repository) };
        backupTask.AddNewTask(backupObjects[0]);
        backupTask.AddNewTask(backupObjects[1]);

        backupTask.Start();
        backupTask.RemoveTask(backupObjects[1]);
        backupTask.Start();

        var merger = new RestorePointMerger(new RestorePointDeleter(repository));

        backupTask.Merge(backupTask.RestorePoints, merger);

        Assert.Single(((IDirectoryEntity)repository.OpenEntity(backupTask.BackupTaskPath)).Entities());
    }

    [Fact]
    public static void TestRestoreToDifferentLocation()
    {
        var repository = new InMemoryRepositorySuper("repo");
        var logger = new ConsoleLogger(true);
        var backupTask = new BackupTaskSuper(new NowTimeStrategy(), repository, new SplitStorageAlgorithmVisitor(new ZipArchiver()), logger);
        repository.CreateDirectory("test1");
        Stream stream1 = repository.CreateFile(@"test1\test2.txt");
        stream1.Write(Encoding.UTF8.GetBytes("Hello, World!"));
        stream1.Close();
        Stream stream2 = repository.CreateFile("test3.txt");
        stream2.Write(Encoding.UTF8.GetBytes("Goodbye, this f**king world!"));
        stream2.Close();
        var backupObjects = new List<BackupObject>() { new BackupObject("test1", repository), new BackupObject("test3.txt", repository) };
        backupTask.AddNewTask(backupObjects[0]);
        backupTask.AddNewTask(backupObjects[1]);

        RestorePoint restorePoint = backupTask.Start();

        backupTask.RestoreBackupToDifferentLocation(restorePoint, "unpack", repository);

        Assert.Equal(2, ((IDirectoryEntity)repository.OpenEntity("unpack")).Entities().Count());
    }

    [Fact]
    public static void TestRestoreToOriginalLocation()
    {
        var repository = new InMemoryRepositorySuper("repo");
        var logger = new ConsoleLogger(true);
        var backupTask = new BackupTaskSuper(new NowTimeStrategy(), repository, new SplitStorageAlgorithmVisitor(new ZipArchiver()), logger);
        repository.CreateDirectory("test1");
        Stream stream1 = repository.CreateFile(@"test1\test2.txt");
        stream1.Write(Encoding.UTF8.GetBytes("Hello, World!"));
        stream1.Close();
        Stream stream2 = repository.CreateFile("test3.txt");
        stream2.Write(Encoding.UTF8.GetBytes("Goodbye, this f**king world!"));
        stream2.Close();
        var backupObjects = new List<BackupObject>() { new BackupObject("test1", repository), new BackupObject("test3.txt", repository) };
        backupTask.AddNewTask(backupObjects[0]);
        backupTask.AddNewTask(backupObjects[1]);

        RestorePoint restorePoint = backupTask.Start();
        repository.DeleteEntity("test1");
        repository.DeleteEntity("test3.txt");
        backupTask.RestoreBackupToOriginalLocation(restorePoint);

        Assert.True(repository.IsDirectory("test1") && repository.IsFile("test3.txt"));
    }
}