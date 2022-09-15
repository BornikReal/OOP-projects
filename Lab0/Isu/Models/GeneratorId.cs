namespace Isu.Models;
public class GeneratorId
{
    private static readonly string Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\UserData.txt";
    public static List<int> GetIdList()
    {
        if (!File.Exists(Path))
            File.Create(Path).Close();
        StreamReader sr;
        sr = new StreamReader(Path);

        var new_list = new List<int>();
        while (!sr.EndOfStream)
        {
            _ = int.TryParse(sr.ReadLine(), out int x);
            new_list.Add(x);
        }

        sr.Close();
        return new_list;
    }

    public static bool CheckAvail(int id)
    {
        return GetIdList().Find(i => i == id) != default;
    }

    public static int Generate()
    {
        List<int> list = GetIdList();
        int new_id;
        if (list.Count != 0)
            new_id = list.Last() + 1;
        else
            new_id = 0;
        while (list.Contains(new_id))
        {
            new_id++;
            if (new_id >= 1000000)
                new_id = 0;
        }

        StreamWriter sr = File.AppendText(Path);
        sr.WriteLine(new_id);
        sr.Close();
        return new_id;
    }
}