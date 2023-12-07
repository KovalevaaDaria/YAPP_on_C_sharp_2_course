namespace FileOperations;

public class CsvWriter
{
    public void WriteCommon(string filePath, string[] headers, Dictionary<int, List<int>> ls)
    {
        using (var sw = new StreamWriter(filePath))
        {
            sw.WriteLine(string.Join(",", headers));
            foreach (KeyValuePair<int, List<int>> data in ls)
            {
                sw.WriteLine($"{data.Key},{string.Join(",", data.Value)}");
            }
        }
    }
    
    public void WriteAges(string filePath, string[] headers, Tuple<List<int>, List<int>> ls)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine(string.Join(",", headers));
            sw.WriteLine($"Female,{string.Join(",", ls.Item1)}");
            sw.WriteLine($"Male,{string.Join(",", ls.Item2)}");
        }
    }
}