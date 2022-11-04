namespace Backups.Algorithms;

public interface IAlgorithm
{
    void CreateBackup();
    void UnpackBackup(string unpackFolder);
}
