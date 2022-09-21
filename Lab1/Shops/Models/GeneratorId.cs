using System.Text.Json;

namespace Shops.Models;
public class GeneratorId
{
    public GeneratorId(string path, int minId = 000000, int maxId = 999999)
    {
        Path = path;
        MinId = minId;
        MaxId = maxId;
    }

    public int MinId { get; }
    public int MaxId { get; }
    public string Path { get; }

    public List<int>? GetIdList()
    {
        try
        {
            string fs = File.ReadAllText(Path);
            return JsonSerializer.Deserialize<List<int>>(fs);
        }
        catch (FileNotFoundException)
        {
            return null;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    public bool CheckAvail(int id)
    {
        List<int>? list = GetIdList();
        if (list == null)
            return false;
        return list.Find(i => i == id) != default;
    }

    public int Generate()
    {
        List<int>? list = GetIdList();
        list ??= new List<int>();

        int newId;
        if (list.Count != 0)
            newId = list.Last() + 1;
        else
            newId = MinId;
        int round = newId;
        while (list.Contains(newId))
        {
            newId++;
            if (newId > MaxId)
                newId = MinId;
            if (newId == round)
                throw new Exception();
        }

        list.Add(newId);
        var fs = new StreamWriter(Path);
        fs.Write(JsonSerializer.Serialize<List<int>>(list));
        fs.Close();
        return newId;
    }

    public void UseID(int id)
    {
        List<int>? list = GetIdList();
        if (list == null)
            return;
        if (!CheckAvail(id))
            throw new Exception();
        list.Add(id);
        var fs = new StreamWriter(Path);
        fs.Write(JsonSerializer.Serialize<List<int>>(list));
        fs.Close();
    }

    public void FreeID(int id)
    {
        List<int>? list = GetIdList();
        if (list == null)
            return;
        list.Remove(id);
        var fs = new StreamWriter(Path);
        fs.Write(JsonSerializer.Serialize<List<int>>(list));
        fs.Close();
    }
}