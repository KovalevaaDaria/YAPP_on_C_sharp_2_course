using LinearAlgebra;

namespace TestFunctionsForVector
{
    [TestFixture]
    public class MathVectorTests
    {
        [Test]
        public void CorrectDimensionsUnitTest()
        {
            double[] values = {1, 2, 3};
            var vector = new MathVector(values);
            Assert.AreEqual(values.Length, vector.Dimensions);
        }
        

        [Test]
        public void IncorrectDimensionsUnitTest()
        {
            Assert.Throws<ArgumentNullException>(() => new MathVector(null));
        }
        
        
        [Test]
        public void ZeroDimensionsUnitTest()
        {
            double[] values = {  };
            var vector = new MathVector(values);
            Assert.AreEqual(0, vector.Dimensions);
        }
        

        [Test]
        public void CorrectGetIndexUnitTest()
        {
            var vector = new MathVector(1, 2, 3);
            var value = vector[1];
            Assert.AreEqual(2, value);
        }
        
        
        [Test]
        public void IncorrectGetIndexUnitTest()
        {
            var vector = new MathVector(1, 2, 3);
            Assert.Throws<IndexOutOfRangeException>(() => { var value = vector[4]; });
        }
        

        [Test]
        public void CorrectSetIndexUnitTest()
        {
            var vector = new MathVector(1, 2, 3);
            vector[1] = 5;
            Assert.AreEqual(5, vector[1]);
        }
        
        
        [Test]
        public void IncorrectSetIndexUnitTest()
        {
            var vector = new MathVector(1, 2, 3);
            Assert.Throws<IndexOutOfRangeException>(() => vector[4] = 4);
        }
        
        
        [Test]
        public void CorrectLenghtUnitTest()
        {
            var vector1 = new MathVector(1, 2, 3);
            var vector2 = new MathVector(2, 3, 1);
            Assert.AreEqual(vector1.Length, vector2.Length);
        }
        
        
        [Test]
        public void ZeroLenghtUnitTest()
        {
            double[] values = {  };
            var vector = new MathVector(values);
            Assert.AreEqual(0, vector.Length);
        }
        

        [Test]
        public void SumNumberUnitTest()
        {
        var vector = new MathVector(1, 2, 3);
        var result = vector.SumNumber(5);
        Assert.AreEqual(new MathVector(6, 7, 8), result);
        }
        
        
        [Test]
        public void NegativeSumNumberUnitTest()
        {
            var vector = new MathVector(-1, -2, -3);
            var result = vector.SumNumber(-5);
            Assert.AreEqual(new MathVector(-6, -7, -8), result);
        }
        
        
        [Test]
        public void MultiplyNumberUnitTest()
        {
        var vector = new MathVector(1, 2, 3);
        var result = vector.MultiplyNumber(2);
        Assert.AreEqual(new MathVector(2, 4, 6), result);
        }
        
        
        [Test]
        public void NegativeMultiplyNumberUnitTest()
        {
            var vector = new MathVector(1, 2, 3);
            var result = vector.MultiplyNumber(-2);
            Assert.AreEqual(new MathVector(-2, -4, -6), result);
        }
        
        
        [Test]
        public void ZeroMultiplyNumberUnitTest()
        {
            var vector = new MathVector(1, 2, 3);
            var result = vector.MultiplyNumber(0);
            Assert.AreEqual(new MathVector(0, 0, 0), result);
        }
        
        
        [Test]
        public void SumVectorsUnitTest()
        {
        var vector1 = new MathVector(1, 2, 3);
        var vector2 = new MathVector(4, 5, 6);
        var result = vector1.Sum(vector2);
        Assert.AreEqual(new MathVector(5, 7, 9), result);
        }
        
        
        [Test]
        public void NegativeSumVectorsUnitTest()
        {
            var vector1 = new MathVector(1, 2, 3);
            var vector2 = new MathVector(-4, -5, -6);
            var result = vector1.Sum(vector2);
            Assert.AreEqual(new MathVector(-3, -3, -3), result);
        }
        
        
        [Test]
        public void ZeroSumVectorsUnitTest()
        {
            var vector1 = new MathVector(1, 2, 3);
            var vector2 = new MathVector(0, 0, 0);
            var result = vector1.Sum(vector2);
            Assert.AreEqual(new MathVector(1, 2, 3), result);
        }
        
        
        [Test]
        public void DifferentDimensionsSumVectorsUnitTest()
        {
            var vector1 = new MathVector(1, 2, 3, 4);
            var vector2 = new MathVector(2, 3, 4);
            Assert.Throws<InvalidOperationException>(() => vector1.Sum(vector2));
        }
        
        
        [Test]
        public void MultiplyVectorsUnitTest()
        {
        var vector1 = new MathVector(1, 2, 3);
        var vector2 = new MathVector(4, 5, 6);
        var result = vector1.Multiply(vector2);
        Assert.AreEqual(new MathVector(4, 10, 18), result);
        }
        
        
        [Test]
        public void NegativeMultiplyVectorsUnitTest()
        {
            var vector1 = new MathVector(1, 2, 3);
            var vector2 = new MathVector(-4, -5, 6);
            var result = vector1.Multiply(vector2);
            Assert.AreEqual(new MathVector(-4, -10, 18), result);
        }
        
        
        [Test]
        public void ZeroMultiplyVectorsUnitTest()
        {
            var vector1 = new MathVector(1, 2, 3);
            var vector2 = new MathVector(0, 0, 0);
            var result = vector1.Multiply(vector2);
            Assert.AreEqual(new MathVector(0, 0, 0), result);
        }
        
        
        [Test]
        public void DifferentDimensionsMultiplyVectorsUnitTest()
        {
            var vector1 = new MathVector(1, 2, 3, 4);
            var vector2 = new MathVector(0, 0, 0);
            Assert.Throws<InvalidOperationException>(() => vector1.Multiply(vector2));
        }
        
        
        [Test]
        public void ScalarMultiplyUnitTest()
        {
        var vector1 = new MathVector(1, 2, 3);
        var vector2 = new MathVector(4, 5, 6);
        var result = vector1.ScalarMultiply(vector2);
        Assert.AreEqual(32, result);
        }
        
        
        [Test]
        public void NegativeScalarMultiplyUnitTest()
        {
            var vector1 = new MathVector(1, 2, 3);
            var vector2 = new MathVector(-4, -5, -6);
            var result = vector1.ScalarMultiply(vector2);
            Assert.AreEqual(-32, result);
        }
        
        
        [Test]
        public void ZeroScalarMultiplyUnitTest()
        {
            var vector1 = new MathVector(1, 2, 3);
            var vector2 = new MathVector(0, 0, 0);
            var result = vector1.ScalarMultiply(vector2);
            Assert.AreEqual(0, result);
        }
        
        
        [Test]
        public void DifferentDimensionsScalarMultiplyUnitTest()
        {
            var vector1 = new MathVector(1, 2, 3, 4);
            var vector2 = new MathVector(0, 0, 0);
            Assert.Throws<InvalidOperationException>(() => vector1.ScalarMultiply(vector2));
        }
        
        
        [Test]
        public void CalcDistanceUnitTest()
        {
        var vector1 = new MathVector(1, 2, 3);
        var vector2 = new MathVector(4, 5, 6);
        var result = vector1.CalcDistance(vector2);
        Assert.AreEqual(5.196152, result, 0.000001); 
        }
        
        
        [Test]
        public void NegativeCalcDistanceUnitTest()
        {
            var vector1 = new MathVector(-1, -2, -3);
            var vector2 = new MathVector(4, 5, 6);
            var result = vector1.CalcDistance(vector2);
            Assert.AreEqual(12.449899, result, 0.000001); 
        }
    }
}