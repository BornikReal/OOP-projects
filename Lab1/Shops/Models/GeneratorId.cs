namespace Shops.Models;
public class GeneratorId
{
    private int _maxId;
    private int _curId;
    public GeneratorId(int minId = 100000, int maxId = 999999)
    {
        _maxId = maxId;
        _curId = minId;
    }

    public int Generate()
    {
        _maxId++;
        if (_curId == _maxId)
            throw new Exception();
        return _curId++;
    }
}