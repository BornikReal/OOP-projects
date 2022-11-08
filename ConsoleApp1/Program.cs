using Backups.Archivator;
using Backups.FileSystemEntities.Interfaces;
using Backups.Repository;

var rep = new FileSystemRepository("aboba");
IFileSystemEntity systemEntity = rep.OpenEntity("C:\\Users\\cooln\\source\\repos\\OOP\\ConsoleApp1\\pack");
var entities = new List<IFileSystemEntity>
{
    systemEntity,
};

var arch = new ZipArchivator();
FileStream stream = File.OpenWrite("C:\\Users\\cooln\\source\\repos\\OOP\\ConsoleApp1\\packZipped.zip");
arch.CreateArchive(entities, stream);
stream.Close();