using Backups.Extra.Wrappers;
using Backups.FileSystemEntities.Interfaces;
using Backups.Visitors;

namespace Backups.Extra.Restorer;

public class RestorerVisitor : IArchiveVisitor
{
    public RestorerVisitor(IRepositorySuper repository)
    {
        Repository = repository;
    }

    public string SavingPath { get; set; } = string.Empty;
    public IRepositorySuper Repository { get; set; }

    public void Visit(IFileEntity fileEnity)
    {
        if (Repository.IsFile(SavingPath))
            Repository.DeleteEntity(SavingPath);
        Stream fileStream = Repository.CreateFile(SavingPath);
        Stream entityStream = fileEnity.FuncStream();
        entityStream.CopyTo(fileStream);
        fileStream.Close();
        entityStream.Close();
    }

    public void Visit(IDirectoryEntity directoryEnity)
    {
        if (Repository.IsDirectory(SavingPath))
            Repository.DeleteEntity(SavingPath);
        Repository.CreateDirectory(SavingPath);
        string oldSavingPath = new string(SavingPath);
        foreach (IFileSystemEntity file in directoryEnity.Entities())
        {
            SavingPath = $"{oldSavingPath}{Repository.PathSeparator}{file.Name}";
            file.Accept(this);
        }
    }
}
