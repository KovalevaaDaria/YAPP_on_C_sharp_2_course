using System.Collections.Concurrent;

namespace BaseClasses;

public class BaseEventEmitter
{
    protected string _name;
    protected TimeSpan _frequency;
    protected Random _random = new Random();
    protected ConcurrentQueue<int> Queue;
    protected CancellationToken CancellationToken;
    protected int TotalGeneratedItems;

    protected BaseEventEmitter(string name, TimeSpan frequency, ConcurrentQueue<int> queue,
        CancellationToken cancellationToken)
    {
        _name = name;
        _frequency = frequency;
        Queue = queue;
        CancellationToken = cancellationToken;
        TotalGeneratedItems = 0;
    }
    
    public virtual async Task GenerateEvents() { }
    
    public int GetTotalGeneratedItems()
    {
        return TotalGeneratedItems;
    }

    public int GetQueueLength()
    {
        return Queue.Count;
    }
}