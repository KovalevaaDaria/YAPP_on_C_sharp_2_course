namespace Manager;
using BaseClasses;

public static class Logger
{
    public static void Log(string message) 
    {
        Console.WriteLine(message);
    }

    public static void LogProcessStart()
    {
        Console.WriteLine("\nПрограмма началась:\n");
    }

    public static void LogProcessFinish()
    {
        Console.WriteLine("\nПроцесс завершён!\n");
    }

    public static void LogMetrics(BaseEventEmitter emitter, IEnumerable<BaseEventHandler> handlers)
    {
        Log($"Поставщиком было сгенерировано {emitter.GetTotalGeneratedItems()} партий");
        Log($"Длина очереди поставщика: {emitter.GetQueueLength()}\n");
        foreach (var handler in handlers)
        {
            var itemsProcessedSorting = handler.EventStartTime.Count;
            var totalProcessingHandleTime = handler.GetTotalProcessingTime();
            var averageProcessingTimeSorting = 
                MetricsCalculator.CalculateAverageProcessingTime(totalProcessingHandleTime, handler.ProcessingTime);
            
            var queueLength = handler.OutputQueue.Count;
            var averageQueueLength = MetricsCalculator.CalculateAverageQueueLength(handler.OutputQueue.Count,
                handler.ProcessingTime);
            LogMetricsForHandler($"{handler.Name}", itemsProcessedSorting, averageProcessingTimeSorting, queueLength, 
                averageQueueLength);
        }
    }

    private static void LogMetricsForHandler(string name, int itemsProcessed, double averageProcessingTime, int queueLength, 
        double averageQueueLength)
    {
        Console.WriteLine($"Метрики для обработчика - {name}:");
        Console.WriteLine($"Обработано партий: {itemsProcessed}");
        Console.WriteLine($"Среднее время обработки: {averageProcessingTime}");
        Console.WriteLine($"Длина выходной очереди: {queueLength}");
        Console.WriteLine($"Средняя длина очереди: {averageQueueLength}\n");
    }
}