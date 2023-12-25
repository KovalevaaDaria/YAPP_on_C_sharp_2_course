namespace Manager;

public static class MetricsCalculator
{
    public static double CalculateAverageProcessingTime(TimeSpan totalProcessingTime, TimeSpan frequency)
    {
        if (frequency == TimeSpan.Zero)
        {
            return 0;
        }

        return totalProcessingTime.TotalMilliseconds / frequency.TotalMilliseconds;
    }

    public static double CalculateAverageQueueLength(int sum, TimeSpan frequency)
    {
        if (frequency == TimeSpan.Zero)
        {
            return 0;
        }
        return sum / frequency.TotalSeconds;
    }
}