using Demographic.Engine;
using FileOperations;

namespace Exec
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 7)
            {
                Console.WriteLine("Ошибка! Недостаточно аргументов командной строки!");
                return;
            }

            var initialAgePath = args[0];
            var deathRulesPath = args[1];
            
            if (!int.TryParse(args[2], out var startDate) ||
                !int.TryParse(args[3], out var endDate) ||
                !int.TryParse(args[4], out var population) ||
                startDate < 0 || endDate < 0 || population < 0)
            {
                Console.WriteLine("Ошибка! Неверные аргументы для даты и населения!");
                return;
            }
            
            var outputFileCommon = args[5];
            var outputFileAges = args[6];

            if (population <= 0)
            {
                Console.WriteLine("Ошибка! Общее населения не может быть меньше нуля!");
                return;
            }

            if (startDate >= endDate)
            {
                Console.WriteLine("Ошибка! Даты моделирования не соответствуют действительности!");
                return;
            }

            try
            {
                Console.WriteLine("\nStart program:\n");
                IEngine engine = new Engine(initialAgePath, deathRulesPath, startDate, endDate, population);
                var metrics = engine.Model();
                var csv = new CsvWriter();
                csv.WriteCommon(outputFileCommon, new[] { "Year", "Population", "female", "male" }, metrics.Metrics);
                csv.WriteAges(outputFileAges, new[] { "Sex", "0-18", "19-45", "45-65", "65-100" }, metrics.MetricsAges);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка! Что-то пошло не так!");
                Console.WriteLine(e.Message);
            }
        }
    }
}