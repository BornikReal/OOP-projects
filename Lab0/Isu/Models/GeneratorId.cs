using System.Text.Json;
using Isu.Exception.IdException;

namespace Isu.Models;
public class GeneratorId
{
    public const int MinId = 100000;
    public const int MaxId = 999999;

    private static readonly string Path = "UserData.json";
    public static List<int>? GetIdList()
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

    public static bool CheckAvail(int id)
    {
        List<int>? list = GetIdList();
        if (list == null)
            return false;
        return list.Find(i => i == id) != default;
    }

    public static int Generate()
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
                throw new UnavailableIdException();
        }

        list.Add(newId);
        var fs = new StreamWriter(Path);
        fs.Write(JsonSerializer.Serialize<List<int>>(list));
        fs.Close();
        return newId;
    }

    public static void FreeID(int id)
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