namespace VectorDemo
{
    public abstract class Program
    {
        private static void Main()
        {
            var test = new TestMathVectorFunctions();
            test.DimensionsTest();
            test.IndexTest();
            test.LengthTest();
            test.SumNumberTest();
            test.MultiplyNumberTest();
            test.SumTest();
            test.MultiplyTest();
            test.ScalarMultiplyTest();
            test.CalcDistanceTest();
        }
    }
}

