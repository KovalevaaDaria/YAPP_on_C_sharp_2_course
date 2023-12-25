using System.Collections.Concurrent;
using BaseClasses;
using Manager;

namespace Handlers;

public class Sorter : BaseEventHandler
{
    private const double ProbabilityOfDefect = 0.2;
    private int _defectCount = 0;

    public Sorter(string name, TimeSpan processingTime, ConcurrentQueue<int> inputQueue,
        ConcurrentQueue<int> outputQueue, CancellationToken cancellationToken) : 
        base(name, processingTime, inputQueue, outputQueue, cancellationToken) { }
    
    public override async Task HandleEvents()
    {
        try
        {
            while (!CancellationToken.IsCancellationRequested)
            {
                while (InputQueue.TryDequeue(out int eventId))
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
                    if (Random.NextDouble() < ProbabilityOfDefect)
                    {
                        var defectMessage = $"{DateTime.Now:HH:mm:ss} - {Name} обнаружил брак в партии {eventId}. " +
                                            $"Товар будет возвращён поставщику";
                        Logger.Log(defectMessage);
                        FileManager.WriteToFile(defectMessage);
                        OutputQueue.TryDequeue(out var defectiveItem);
                        _defectCount++;
                        Logger.Log($"Длина очереди на вход: {InputQueue.Count}, на выход: {OutputQueue.Count}");
                    }
                }
                
                await Task.Delay(100, CancellationToken);
            }
            Logger.Log("Генерация и обработка событий окончена! Для подсчёта статистики нажмите RETURN!\n");
        }
        catch (TaskCanceledException)
        {
            Logger.Log($"Смена для {Name.ToLower()} закончилась!");
        }
    }

    public int GetDefectCount()
    {
        return _defectCount;
    }
}