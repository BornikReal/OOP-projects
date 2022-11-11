﻿using System.IO.Compression;
using Backups.FileSystemEntities.Interfaces;
using Backups.Visitors;

namespace Backups.Archiver;

public class ZipArchivator : IArchivator
{
    public string Archiveextension { get; } = ".zip";

    public void CreateArchive(List<IFileSystemEntity> entities, Stream archiveStream)
    {
        using var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create);
        var visitor = new ZipVisitor(archive);
        foreach (IFileSystemEntity entity in entities)
            entity.Accept(visitor);
    }
}