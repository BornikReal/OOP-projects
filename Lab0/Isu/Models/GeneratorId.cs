using Isu.Exception;

namespace Isu.Models;
public class GeneratorId
{
    private readonly int _maxId;
    private int _curId;

    public GeneratorId(int minId = 100000, int maxId = 999999)
    {
        _maxId = maxId;
        _curId = minId;
    }

    public int Generate()
    {
        if (_curId == _maxId)
            throw new UnavailableIdException();
        return _curId++;
    }
}