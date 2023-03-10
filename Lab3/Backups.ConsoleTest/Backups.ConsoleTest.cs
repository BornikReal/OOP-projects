using Backups.Algorithms;
using Backups.Archiver;
using Backups.FileSystemEntities;
using Backups.Interlayer;
using Backups.Models;
using Backups.Repository;
using Backups.Strategy;
using System.Diagnostics;
using System.Text;

var repo = new RealRepository("C:\\Users\\cooln\\source\\repos\\OOP\\Backups.ConsoleTest\\repo");
var elonTask = new BackupTask(new NowTimeStrategy(), repo, new SplitStorageAlgorithm(new ZipArchiver()), new Backup());
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
RestorePoint restorePoint = elonTask.Start();
IRepoDisposable repDis1 = restorePoint.Storage.GetEntities();
var entities = repDis1.Entities.ToList();
Stream stream3 = ((FileEntity)entities[1]).FuncStream();
Debug.Assert("Goodbye, this f**king world!" == new StreamReader(stream3).ReadToEnd());
stream3.Close();
repDis1.Dispose();