using System.Collections;

namespace FileOperations.Rules;

public class InitialAge : IRules
{
    private Dictionary<int, double> _ages;
    
    public InitialAge(string path)
    {

        if(!path.Contains(".csv"))
            throw new FileNotFoundException("Файл не имеет .csv расширения");
        
        if (!File.Exists(path))
            throw new FileNotFoundException("Файл не найден");
        
        if (new FileInfo(path).Length > Int32.MaxValue)
            throw new FileLoadException("Файл слишком большой");
        
        _ages = new Dictionary<int, double>();
        
        using var reader = new StreamReader(path);
        var headers = reader.ReadLine();
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line?.Split(", ");

            if (values == null)
                throw new Exception($"{path} файл содержит некоректные заголовки!");
            
            try
            {
                var key = Convert.ToInt32(values[0]);
                var val = Convert.ToDouble(values[1].Replace('.',','));
                _ages.Add(key, val);
            }
            catch (FormatException)
            {
                throw new FormatException("Входная строка не является последовательностью цифр!");
            }
            catch (OverflowException)
            {
                throw new OverflowException("Число не умещается в Int32!");
            }
        }
    }
    
    public double Get(int key)
    {
        return _ages[key];
    }

    public IEnumerator GetEnumerator()
    {
        return _ages.GetEnumerator();
    }
}