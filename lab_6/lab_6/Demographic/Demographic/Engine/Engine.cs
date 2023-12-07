using Demographic.events;
using Demographic.metrics;
using Demographic.Person;
using FileOperations.Rules;

namespace Demographic.Engine;

public class Engine : IEngine
{
    private const int Accuracy = 10000;
    private readonly int _population;
    private readonly int _startDate;
    private readonly int _endDate;
    private readonly InitialAge _initialAge;
    private readonly DeathRules _deathRules;

    private readonly YearTickEvent _event;
    
    public Engine(string initialAgePath, 
                  string deathRulesPath, int startDate = 1970, 
                  int endDate = 2021, 
                  int population = 130_000_000)
    {
        _event = new YearTickEvent();
        _children = new List<IPerson>();
        _startDate = startDate;
        _endDate = endDate;
        _population = population;
        _initialAge = new InitialAge(initialAgePath);
        _deathRules = new DeathRules(deathRulesPath);
    }

    private Dictionary<int, int> GetGroups()
    {
        var groups = new Dictionary<int, int>();
        foreach (KeyValuePair<int, double> line in _initialAge)
        {
            // var amountGroup = (int)(Math.Round(line.Value / 2.0) * 2);
            var amountGroup = (int)(Math.Round(line.Value));
            groups.Add(line.Key, amountGroup);
        }
        
        return groups;
    }
    
    private List<IPerson> GeneratePeople()
    {
        var people = new List<IPerson>();
        var half = (int) Math.Floor((double)_population / 2) / Accuracy;
        for (var i = 0; i < half; i += Accuracy)
        {
            var groups = GetGroups();
            foreach (var line in groups)
            {
                for (var n = 0; n < line.Value; n++)
                {
                    var birthDate = _startDate - line.Key;
                    var female = new Female(birthDate, _event);
                    var male = new Male(birthDate, _event);
                    people.Add(male);
                    people.Add(female);
                    female.ChildBirth += AddChild;
                }
            }
        }

        return people;
    }

    private List<IPerson> _children;
    private void AddChild(IPerson child)
    {
        if (child is Female fm)
        {
            fm.ChildBirth += AddChild;
        }
        _children.Add(child);
    }
    
    private void Deregister(IPerson person)
    {
        if (person is Female fm && person.IsDead())
        {
            fm.ChildBirth -= AddChild;
        }
    }
    
    public MetricsHolder Model()
    {
        var people = GeneratePeople();
        var metrics = new MetricsHolder(_startDate, people);
        
        for (var date = _startDate; date <= _endDate; date++)
        {
            _event.OnEvent(date, _deathRules);
            
            people.ForEach(p => Deregister(p));
            var died = people.RemoveAll(person => person.IsDead());
            people.AddRange(_children);

            var sexCounter = metrics.CountSex();
            metrics.Metrics.Add(date + 1, new List<int>()
            {
                people.Count, sexCounter.Item1, sexCounter.Item2
            });
            Console.WriteLine($"Год - {date}, Население - {people.Count * Accuracy}, Родилось - {_children.Count}, Умерло - {died}");
            _children.Clear();
        }
        metrics.CountAges(_endDate);
        Console.WriteLine("\nМетрики:");
        Console.WriteLine($"Итоговое население - {people.Count * Accuracy}");
        Console.WriteLine($"Было в начале - {_population}");
        return metrics;
    }
}

public static class ProbabilityCalculator
{
    private static readonly Random _random = new Random();

    public static bool IsEventHappened(double eventProbability)
    {
        return _random.NextDouble() <= eventProbability;
    }
}