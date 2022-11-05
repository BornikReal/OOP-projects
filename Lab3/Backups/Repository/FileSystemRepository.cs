﻿using Backups.FileSystemEntities;
using Backups.Models;

namespace Backups.Repository;

public class FileSystemRepository : IRepository
{
    public FileSystemRepository(string repPath)
    {
        RepositoryPath = repPath;
    }

    public string RepositoryPath { get; }

    public bool IsDirectory(string entityPath)
    {
        return Directory.Exists(entityPath);
    }

    public bool IsFile(string entityPath)
    {
        return File.Exists(entityPath);
    }

    public FileEntity OpenFile(string filePath, bool isOpenStream = false)
    {
        if (!IsFile(filePath))
            throw new Exception();
        Stream? stream = null;
        if (isOpenStream)
            stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        return new FileEntity(Path.GetFileName(filePath), filePath, stream);
    }

    public DirectoryEntity OpenDirectory(string dirPath)
    {
        if (!IsDirectory(dirPath))
            throw new Exception();
        var entitiesList = new List<IFileSystemEntity>();
        var dirInfo = new DirectoryInfo(dirPath);
        foreach (FileInfo file in dirInfo.GetFiles())
            entitiesList.Add(OpenFile(file.FullName));

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            entitiesList.Add(OpenDirectory(dir.FullName));

        return new DirectoryEntity(Path.GetFileName(dirPath), dirPath, entitiesList);
    }

    public IFileSystemEntity OpenEntity(string entityPath)
    {
        if (IsFile(entityPath))
            return OpenFile(entityPath, true);
        else if (IsDirectory(entityPath))
            return OpenDirectory(entityPath);
        throw new Exception();
    }

    public string CreateBackupTaskDirectory(BackupTask backupTask)
    {
        string backupTaskDirectory = RepositoryPath + Path.DirectorySeparatorChar + "BackupTask-" + backupTask.Id;
        if (IsDirectory(backupTaskDirectory))
            throw new Exception();
        Directory.CreateDirectory(backupTaskDirectory);
        return backupTaskDirectory;
    }

    public string CreateRestorePointDirectory(RestorePoint restorePoint)
    {
        string restorePointDirectory = RepositoryPath + Path.DirectorySeparatorChar + "RestorePoint-" + restorePoint.Id;
        if (IsDirectory(restorePointDirectory))
            throw new Exception();
        Directory.CreateDirectory(restorePointDirectory);
        return restorePointDirectory;
    }
}
