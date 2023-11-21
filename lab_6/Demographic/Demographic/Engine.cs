using FileOperations;

namespace Demographic
{
    public class Engine : IEngine
    {
        public event EventHandler YearTick;

        protected virtual void OnYearTick()
        {
            YearTick?.Invoke(this, EventArgs.Empty);
        }

        public int BeginYear;
        public int EndYear;
        public int PopulationStart;
        public string ToInitialAgePath;
        public string ToDeathRulesPath { get; }
        public List<Person> People { get; set; }
        public List<RowDR> DataDeath;

        public Dictionary<string, List<int>> YearPopulation;
        public Dictionary<string, List<int>> GroupedByAgePeople;

        public Engine(string fileInitial, string fileDeath, int begin, int end, int population)
        {
            BeginYear = begin;
            EndYear = end;
            PopulationStart = population / 1000;
            ToInitialAgePath = fileInitial;
            ToDeathRulesPath = fileDeath;
            YearPopulation = new Dictionary<string, List<int>>() { { begin.ToString(), new List<int>() { PopulationStart, PopulationStart / 2, PopulationStart / 2 } } };

            try
            {
                People = InitialPopulation();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Person> InitialPopulation()
        {
            var people = new List<Person>();
            var init = new InitialAge(ToInitialAgePath);
            var data = init.GetDataInitAge();
            var populationCount = new List<int> { 0, 0, 0 };
            if (populationCount == null) 
                throw new ArgumentNullException(nameof(populationCount));
            Console.WriteLine("Init Start");
            try
            {
                for (var n = 0; n < PopulationStart / 2000; n++)
                {
                    foreach (var line in data)
                    {
                        for (var i = 0; i < line.Value; i++)
                        {
                            var age = BeginYear - line.Key;
                            people.Add(new Person('m', age, this));
                            people.Add(new Person('f', age, this));
                        }
                    }
                }
                People = people;
                populationCount[0] = People.Count;
                populationCount[1] = People.Count / 2;
                populationCount[2] = People.Count / 2;

                DeathRules deathRules = new DeathRules(ToDeathRulesPath);
                DataDeath = deathRules.GetDataDeath();
                Console.WriteLine($"Number of People in the beginning: {People.Count}");
                return people;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Model()
        {
            Console.WriteLine("Model Start");
            var yearPopulation = new List<int>() { People.Count, People.Count / 2, People.Count / 2 };
            for (var i = BeginYear + 1; i < EndYear + 1; i++)
            {
                YearPopulation.Add(i.ToString(), yearPopulation);
                var children = new List<Person>();
                var diedPeople = new List<Person>();
                foreach (var person in People)
                {
                    var child = person.BornChild(i);
                    if (child != null)
                    {
                        children.Add(child);
                        yearPopulation[0]++;
                        if (child.Sex == 'f') yearPopulation[2]++;
                        else yearPopulation[1]++;
                    }
                    person.DethDecision(i, DataDeath);
                    if (person.IsAlive == false)
                    {
                        yearPopulation[0]--;
                        if (person.Sex == 'f') yearPopulation[2]--;
                        else yearPopulation[1]--;
                        diedPeople.Add(person);
                    }
                }
                OnYearTick();

                Console.WriteLine($"Year: {i}, Died: {diedPeople.Count.ToString()}, Born: {children.Count.ToString()}");
                People.RemoveAll(person => diedPeople.Contains(person));
                People.AddRange(children);
                yearPopulation = new List<int>() { People.Count, YearPopulation[i.ToString()][1], YearPopulation[i.ToString()][2] };
            }

            Console.WriteLine($"YearPopulation in the beginning is {YearPopulation[BeginYear.ToString()][0]}, YearPopulation in the end is {YearPopulation[EndYear.ToString()][0]}");
            Console.WriteLine($"Difference = {YearPopulation[EndYear.ToString()][0] - YearPopulation[BeginYear.ToString()][0]}");
            GroupingPeople();
        }


        public void GroupingPeople()
        {
            Dictionary<string, List<int>> ageGroups = new Dictionary<string, List<int>>
            {
                {"0-18", new List<int>{0, 0, 0}},
                {"19-44", new List<int>{0, 0, 0}},
                {"45-64", new List<int>{0, 0, 0}},
                {"65-100", new List<int>{0, 0, 0}}
            };
            GroupedByAgePeople = ageGroups;
            foreach (var person in People)
            {
                var age = EndYear - person.BirthYear;
                if (age < 65)
                {
                    if (age < 45)
                    {
                        if (age < 19)
                            PopulationCount("0-18", person.Sex);
                        else
                            PopulationCount("19-44", person.Sex);

                    }
                    else
                        PopulationCount("45-64", person.Sex);
                }
                else
                {
                    PopulationCount("65-100", person.Sex);
                }
            }
        }

        public void PopulationCount(string key, char gender)
        {
            GroupedByAgePeople[key][0]++;
            if (gender == 'f') GroupedByAgePeople[key][2]++;
            else GroupedByAgePeople[key][1]++;
        }
    }
}