namespace FileOperations
{
    public class WriteFile : IWriteFile
    {
        public void WriteData(string filename, string headers, Dictionary<string, List<int>> content)
        {
            using var sw = new StreamWriter(filename);
            sw.WriteLine(headers);
            foreach (var data in content)
            {
                var csvLine = string.Join(",", data.Value);
                sw.WriteLine($"{Convert.ToString(data.Key)},{csvLine}");
            }
        }
    }
}