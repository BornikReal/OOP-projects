using Isu.Exception.IdException;

namespace Isu.Models;
public class GeneratorId
{
    public const int MinId = 100000;
    public const int MaxId = 999999;

    private static readonly string Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\UserData.txt";
    public static List<int> GetIdList()
    {
        if (!File.Exists(Path))
            File.Create(Path).Close();
        StreamReader sr;
        sr = new StreamReader(Path);

        var newList = new List<int>();
        while (!sr.EndOfStream)
        {
            _ = int.TryParse(sr.ReadLine(), out int x);
            newList.Add(x);
        }

        sr.Close();
        return newList;
    }

    public static bool CheckAvail(int id)
    {
        return GetIdList().Find(i => i == id) != default;
    }

    public static int Generate()
    {
        List<int> list = GetIdList();
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

        StreamWriter sr = File.AppendText(Path);
        sr.WriteLine(newId);
        sr.Close();
        return newId;
    }
}