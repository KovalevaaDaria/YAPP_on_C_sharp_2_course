using FileOperations.Rules;

namespace Demographic.Person;

public interface IPerson
{
    public void NextCycle(int date, DeathRules rule);

    public bool IsDead();

    public bool SetDeath(int deathDate);

    public int Age(int date);
}