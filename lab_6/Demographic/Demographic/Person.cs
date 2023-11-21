using System;
using FileOperations;

namespace Demographic
{
    public class Person
    {
        public event EventHandler<Person> ChildBirth;

        public char Sex { get; } 
        public int BirthYear { get; }
        public int Age { get; private set; }
        public int? DeathYear { get; private set; }
        public bool IsAlive => DeathYear == null;

        private Engine engine;

        public Person(char sex, int birthYear, Engine engine)
        {
            Sex = sex;
            BirthYear = birthYear;
            this.engine = engine;
            engine.YearTick += HandleYearTick;
        }

        public void HandleYearTick(object sender, EventArgs e)
        {
            Age++;
        }
        

        protected virtual void OnChildBirth(Person child)
        {
            ChildBirth?.Invoke(this, child);
        }

        public void DethDecision(int year, List<RowDR> DeathRules)
        {
            int age = year - BirthYear;
            bool die = false;
            for (int i = 0; i < DeathRules.Count; i++)
            {
                if (DeathRules[i].BeginAge < age && age < DeathRules[i].EndAge)
                {
                    if (ProbabilityCalculator.IsEventHappened((Sex == 'f')
                            ? DeathRules[i].WemanDeathProb
                            : DeathRules[i].ManDeathProb))
                    {
                        DeathYear = year;
                    }
                    break;
                }
            }
        }

        public Person BornChild(int year)
        {
            var age = year - BirthYear;
            if (IsAlive && Sex == 'f' && age >= 18 && age <= 45)
            {
                if (ProbabilityCalculator.IsEventHappened(0.151))
                {
                    var genderChild = ProbabilityCalculator.IsEventHappened(0.55) ? 'f' : 'm';
                    Person child = new Person(genderChild, year, engine);
                    OnChildBirth(child);

                    return child;
                }
            }
            return null;
        }
    }
}
