using System.IO.Compression;

namespace Backups.Repository;

public class FileSystemRepository : IRepository, IDisposable
{
    private readonly string tempDirectory;
    private bool disposed = false;
    public FileSystemRepository(string path)
    {
        ArchiveStoragePath = path;
        tempDirectory = Path.GetTempPath() + Path.DirectorySeparatorChar + "RepositoryTempDirectory-" + Guid.NewGuid();
        Directory.CreateDirectory(tempDirectory);
    }

    public string ArchiveStoragePath { get; }

    public static void ExtractArchive(string archiveName, string unpackFolderName)
    {
        ZipFile.ExtractToDirectory(archiveName, unpackFolderName);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void AddEntryToArchive(string entryName)
    {
        if (IsDirectory(entryName))
            Copy(entryName, tempDirectory + Path.DirectorySeparatorChar + Path.GetFileName(entryName));
        else
            File.Copy(entryName, tempDirectory + Path.DirectorySeparatorChar + Path.GetFileName(entryName));
    }

    public void Reset()
    {
        Directory.Delete(tempDirectory, true);
        Directory.CreateDirectory(tempDirectory);
    }

    public void GetArchive(string archiveName)
    {
        ZipFile.CreateFromDirectory(tempDirectory, archiveName);
        Directory.Delete(tempDirectory, true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
            return;

        if (disposing)
            Directory.Delete(tempDirectory, true);

        disposed = true;
    }

    private static void Copy(string sourceDirectory, string targetDirectory)
    {
        var diSource = new DirectoryInfo(sourceDirectory);
        var diTarget = new DirectoryInfo(targetDirectory);
        CopyAll(diSource, diTarget);
    }

    private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
    {
        Directory.CreateDirectory(target.FullName);

        foreach (FileInfo fi in source.GetFiles())
        {
            fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
        }

        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
        {
            CopyAll(diSourceSubDir, target.CreateSubdirectory(diSourceSubDir.Name));
        }
    }

    private static bool IsDirectory(string path)
    {
        return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
    }

    private void AddFolderToArchive(string folderName)
    {
        Copy(folderName, tempDirectory + Path.DirectorySeparatorChar + Path.GetFileName(folderName));
    }

    private void AddFileToArchive(string fileName)
    {
        File.Copy(fileName, tempDirectory + Path.DirectorySeparatorChar + Path.GetFileName(fileName));
    }
}
