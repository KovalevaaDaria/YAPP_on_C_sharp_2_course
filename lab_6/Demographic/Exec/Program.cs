using Demographic;
using FileOperations;

namespace Exec
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            const string initialAgeFileName = "/Users/daria/Documents/Бауманка предметы/2 курс/3 семестр/ЯПП/С#/lab_6/Demographic/files/InitialAge.csv";
            const string deathRulesFileName = "/Users/daria/Documents/Бауманка предметы/2 курс/3 семестр/ЯПП/С#/lab_6/Demographic/files/DeathRules.csv";
            const int startYear = 1970;
            const int endYear = 2000;
            const int populationBegin = 130000000;
                
            try
            {
                var engine = new Engine(initialAgeFileName, deathRulesFileName, startYear, endYear, populationBegin);
                Console.WriteLine("Init End");
                engine.Model();
                Console.WriteLine("Model End");
                IWriteFile writing = new WriteFile();      
                writing.WriteData("YearPopulationFile.csv", "Year,Population,Men,Women", engine.YearPopulation);
                writing.WriteData("AgePopulation.csv", "Age,Population,Men,Women", engine.GroupedByAgePeople);
                Console.WriteLine("Success!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}