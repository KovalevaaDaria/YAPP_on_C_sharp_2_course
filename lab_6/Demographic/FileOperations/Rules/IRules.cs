using System.Collections;

namespace FileOperations.Rules;

public interface IRules : IEnumerable
{
    double Get(int key);
}