using Backups.Algorithms;
using Backups.Archiver;
using Backups.Models;
using Backups.Repository;

var repo = new RealRepository("C:\\Users\\cooln\\source\\repos\\OOP\\ConsoleApp1\\repo");
IBackupTask elonTask = repo.CreateBackupTask(new SingleStorageAlgorithm(), new ZipArchiver());
repo.CreateDirectory("test1");
repo.CreateFile(@"test1\test2.txt").Close();
repo.CreateFile("test3.txt").Close();
var backupObjects = new List<BackupObject>() { new BackupObject("test1", repo), new BackupObject("test3.txt", repo) };
elonTask.AddNewTask(backupObjects[0]);
elonTask.AddNewTask(backupObjects[1]);
elonTask.Start();