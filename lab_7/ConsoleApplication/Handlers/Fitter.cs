using System.Collections.Concurrent;
using BaseClasses;
using Manager;

namespace Handlers;

public class Fitter : BaseEventHandler
{
    public Fitter(string name, TimeSpan processingTime, ConcurrentQueue<int> inputQueue,
        ConcurrentQueue<int> outputQueue, CancellationToken cancellationToken) :
        base(name, processingTime, inputQueue, outputQueue, cancellationToken) { }
    
    public override async Task HandleEvents()
    {
        try
        {
            while (!CancellationToken.IsCancellationRequested)
            {
                while (InputQueue.TryDequeue(out var eventId))
                {
                    if (CancellationToken.IsCancellationRequested)
                        break;

                    await Task.Delay((int)ProcessingTime.TotalMilliseconds + Random.Next(-200, 200),
                        CancellationToken);
                    EventStartTime[eventId] = DateTime.Now;
                    var message = $"{DateTime.Now:HH:mm:ss} - {Name} обработал партию {eventId}";
                    Logger.Log(message);
                    FileManager.WriteToFile(message);
                    OutputQueue.Enqueue(eventId);
                    QueueSum++;
                    Logger.Log($"Длина очереди на вход: {InputQueue.Count}, на выход: {OutputQueue.Count}");
                }
                
                await Task.Delay(100, CancellationToken);
            }
        }
        catch (TaskCanceledException)
        {
            Logger.Log($"Смена для {Name.ToLower()} закончилась!");
        }
    }
}