using FileOperations.Rules;

namespace Demographic.events;

public class YearTickEvent
{
    public delegate void LifeCycleDelegate
    (
        int currentDate,
        DeathRules deathRules
    );
    public event LifeCycleDelegate? YearTick;

    public void OnEvent( int currentDate, DeathRules deathRules)
    {
        YearTick?.Invoke(currentDate, deathRules);
    }
}