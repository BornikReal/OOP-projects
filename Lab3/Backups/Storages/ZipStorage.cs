﻿using Backups.FileSystemEntities.Interfaces;

namespace Backups.Storages;

public class ZipStorage : IStorage
{
    public ZipStorage(IEnumerable<IFileSystemEntity> entities)
    {
        Entities = entities;
    }

    public IEnumerable<IFileSystemEntity> Entities { get; }
}