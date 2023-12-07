using Demographic.metrics;

namespace Demographic.Engine;

public interface IEngine
{
    MetricsHolder Model();
}