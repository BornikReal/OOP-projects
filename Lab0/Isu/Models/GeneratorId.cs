using Isu.Exception;

namespace Isu.Models;
public class GeneratorId
{
    private static readonly int _maxId;
    private static int _curId = 100000;
    public static int Generate()
    {
        if (_curId == _maxId)
            throw new UnavailableIdException();
        return _curId++;
    }
}