using System.Text.Json;
using Isu.Exception.IdException;

namespace Isu.Models;
public class GeneratorId
{
    public const int MinId = 100000;
    public const int MaxId = 999999;

    private static readonly string Path = "UserData.json";

    public static int Generate()
    {
        List<int>? list;
        try
        {
            list = JsonSerializer.Deserialize<List<int>>(File.ReadAllText(Path));
            list ??= new List<int>();
        }
        catch (FileNotFoundException)
        {
            list = new List<int>();
        }
        catch (JsonException)
        {
            list = new List<int>();
        }

        int newId;
        if (list.Count != 0)
            newId = list.Last() + 1;
        else
            newId = MinId;
        if (newId is < MinId or > MaxId)
            throw new UnavailableIdException();

        list.Add(newId);
        var fs = new StreamWriter(Path);
        fs.Write(JsonSerializer.Serialize<List<int>>(list));
        fs.Close();
        return newId;
    }
}