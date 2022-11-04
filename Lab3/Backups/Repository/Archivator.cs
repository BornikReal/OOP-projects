namespace Backups.Repository;

public class Archivator : IDisposable
{
    private readonly FileSystemRepository _repository;
    private bool disposed = false;

    public Archivator(string backupsFolder, FileSystemRepository repository)
    {
        _repository = repository;
        BackupsFolder = backupsFolder;
    }

    public string BackupsFolder { get; }

    public void AddEntryToArchive(string enrtyPath)
    {
        if (FileSystemRepository.IsDirectory(enrtyPath))
            _repository.AddFolderToArchive(enrtyPath);
        else
            _repository.AddFileToArchive(enrtyPath);
    }

    public void MakeArchive(string archiveName)
    {
        _repository.GetArchive(BackupsFolder + Path.DirectorySeparatorChar + archiveName);
    }

    public void Unarchive(string archiveName, string unpackFolder)
    {
        FileSystemRepository.ExtractArchive(BackupsFolder + Path.DirectorySeparatorChar + archiveName, unpackFolder);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
            return;

        if (disposing)
            _repository.Dispose();

        disposed = true;
    }
}
