using Demographic.Person;

namespace Demographic.metrics;

public class MetricsHolder
{
    private readonly List<IPerson> _people;
    public Dictionary<int, List<int>> Metrics;
    public Tuple<List<int>, List<int>> MetricsAges = null!;

    public MetricsHolder(int startDate, List<IPerson> people)
    {
        _people = people;

        var sexCounter = CountSex();
        Metrics = new Dictionary<int, List<int>>()
        {
            {startDate, new List<int>(){_people.Count, sexCounter.Item1, sexCounter.Item2}}
        };
    }

    public void CountAges(int endDate)
    {
        var femalesAges = new List<int>(){0, 0, 0, 0};
        var malesAges = new List<int>(){0, 0, 0, 0};
        foreach (var person in _people)
        {
            var age = person.Age(endDate);
            var i = 0;
            if (age is >= 19 and <= 44)
                i = 1;
            else if (age is >= 45 and <= 65)
                i = 2;
            else if (age is >= 66 and <= 100)
                i = 3;
            if (person is Male)
                malesAges[i]++;
            else
                femalesAges[i]++;
        }

        MetricsAges = new Tuple<List<int>, List<int>>(femalesAges, malesAges);
    }
    
    public Tuple<int, int> CountSex()
    {
        var females = 0;
        var males = 0;
        foreach (var person in _people)
        {
            if (person is Male)
                males++;
            else
                females++;
        }

        return new Tuple<int, int>(females, males);
    }
}