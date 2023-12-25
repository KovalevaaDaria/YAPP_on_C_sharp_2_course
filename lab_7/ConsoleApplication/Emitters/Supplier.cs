using BaseClasses;
using Manager;
using System.Collections.Concurrent;

namespace Emitters;

public class Supplier : BaseEventEmitter
{
    private const int MaxGeneratedEvents = 10;
    
    public Supplier(string name, TimeSpan frequency, ConcurrentQueue<int> queue,
        CancellationToken cancellationToken) : base(name, frequency, queue, cancellationToken) { }

    public override async Task GenerateEvents()
    {
        var eventId = 1;

        try
        {
            while (!CancellationToken.IsCancellationRequested && eventId <= MaxGeneratedEvents)
            {
                await Task.Delay((int)_frequency.TotalMilliseconds + _random.Next(-200, 200), CancellationToken);

                if (CancellationToken.IsCancellationRequested)
                {
                    break;
                }

                var message = $"{DateTime.Now:HH:mm:ss} - {_name} сгенерировал партию {eventId}";
                Logger.Log(message);
                FileManager.WriteToFile(message);
                Queue.Enqueue(eventId); // Добавление номера сгенерированной партии в очередь поставщика
                Logger.Log($"Длина очереди эмиттера: {Queue.Count}");
                eventId++;
                TotalGeneratedItems++;
            }
        }
        catch (TaskCanceledException)
        {
            Logger.Log($"\n\nСмена для {_name.ToLower()} закончилась!");
        }
    }
}