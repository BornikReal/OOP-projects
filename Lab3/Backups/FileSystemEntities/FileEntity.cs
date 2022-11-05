namespace Backups.FileSystemEntities;

public class FileEntity : IFileEntity
{
    private Stream? _stream;
    public FileEntity(string name, string fullPath, Stream? stream)
    {
        Name = name;
        FullPath = fullPath;
        Stream = stream;
    }

    public string Name { get; }

    public string FullPath { get; }

    public Stream? Stream
    {
        get => _stream;
        set
        {
            if (_stream != null)
                _stream.Close();
            _stream = value;
        }
    }
}
