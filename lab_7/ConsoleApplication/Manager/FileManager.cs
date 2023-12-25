namespace Manager;

public static class FileManager
{
    public static void WriteToFile(string message)
    {
        const string filePath = "/Users/daria/Documents/Бауманка предметы/2 курс/3 семестр/ЯПП/С#/lab_7/ConsoleApplication/Manager/Data/log.txt";
        File.AppendAllText(filePath, $"{message}\n");
    }
}