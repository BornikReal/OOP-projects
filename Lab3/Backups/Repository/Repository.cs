using System.IO.Compression;

namespace Backups.Repository;

public class Repository : IRepository
{
    private readonly string tempDirectory;
    public Repository(string path)
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

    public void Init()
    {
        Directory.CreateDirectory(tempDirectory);
    }

    public void AddFolderToArchive(string folderName)
    {
        Copy(folderName, tempDirectory + Path.DirectorySeparatorChar + Path.GetFileName(folderName));
    }

    public void AddFileToArchive(string fileName)
    {
        File.Copy(fileName, tempDirectory + Path.DirectorySeparatorChar + Path.GetFileName(fileName));
    }

    public void Free()
    {
        Directory.Delete(tempDirectory, true);
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
}
