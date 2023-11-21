namespace Demographic
{
    public interface IEngine
    {
        List<Person> InitialPopulation();
        void Model();
    }
}