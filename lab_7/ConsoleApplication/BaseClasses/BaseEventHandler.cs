using System.Collections.Concurrent;

namespace BaseClasses;

public abstract class BaseEventHandler
{
    public readonly string Name;
    public TimeSpan ProcessingTime;
    protected readonly ConcurrentQueue<int> InputQueue;
    public readonly ConcurrentQueue<int> OutputQueue;
    protected readonly Random Random = new Random();
    protected CancellationToken CancellationToken;
    public readonly ConcurrentDictionary<int, DateTime> EventStartTime = new ConcurrentDictionary<int, DateTime>();
    public int QueueSum = 0;
    protected BaseEventHandler(string name, TimeSpan processingTime, ConcurrentQueue<int> inputQueue,
        ConcurrentQueue<int> outputQueue, CancellationToken cancellationToken)
    {
        Name = name;
        ProcessingTime = processingTime;
        InputQueue = inputQueue;
        OutputQueue = outputQueue;
        CancellationToken = cancellationToken;
    }
    
    public virtual async Task HandleEvents() {}
    
    public TimeSpan GetTotalProcessingTime()
    {
        var totalProcessingTime = TimeSpan.Zero;

        foreach (var pair in EventStartTime)
        {
            var start = pair.Value;
            var end = DateTime.Now;
            var processingTime = end - start;
            totalProcessingTime += processingTime;
        }
        
        return totalProcessingTime;
    }
}