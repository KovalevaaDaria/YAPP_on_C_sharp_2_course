using LinearAlgebra;

namespace VectorDemo
{
    public class TestMathVectorFunctions
    {
        IMathVector vector1 = new MathVector(1, 2, 3);
        IMathVector vector2 = new MathVector(4, 5, 6, 7);
        IMathVector vector3 = new MathVector();
        
        
        public void DimensionsTest()
        {
            try
            {
                var dimensions1 = vector1.Dimensions;
                var dimensions2 = vector2.Dimensions;
                var dimensions3 = vector3.Dimensions;
    
                Console.WriteLine("\nDimension Test:");
                Console.WriteLine($"Vector1: {vector1}");
                Console.WriteLine($"Vector2: {vector2}");
                Console.WriteLine($"Vector3: {vector3}");
                Console.WriteLine($"Vector1 dimension is: {dimensions1}");
                Console.WriteLine($"Vector2 dimension is: {dimensions2}");
                Console.WriteLine($"Vector3 dimension is: {dimensions3}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        

        public void IndexTest()
        {
            try
            {
                var value1 = vector1[0];
                var value2 = vector1[1];
                var value3 = vector1[2];

                Console.WriteLine("\nIndex Test:");
                Console.WriteLine($"Vector1: {vector1}");
                Console.WriteLine($"The value of the Vector1[0] = {value1}");
                Console.WriteLine($"The value of the Vector1[1] = {value2}");
                Console.WriteLine($"The value of the Vector1[2] = {value3}");
                Console.WriteLine($"The value of the vector at index[3] = {vector1[3]}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }


        public void LengthTest()
        {
            Console.WriteLine("\nLenght Test:");
            Console.WriteLine($"Vector1: {vector1}");
            Console.WriteLine($"Vector3: {vector3}");
            Console.WriteLine($"The lenght of Vector1 = {vector1.Length}");
            Console.WriteLine($"The lenght of Vector3 = {vector3.Length}");
        }


        public void SumNumberTest()
        {
            var result = vector1.SumNumber(2);
            
            Console.WriteLine("\nSumNumber Test:");
            Console.WriteLine($"Vector1: {vector1}");
            Console.WriteLine($"The amount of Vector1 + 1 = {result}");
        }


        public void MultiplyNumberTest()
        {
            var result = vector1.MultiplyNumber(4);
            
            Console.WriteLine("\nMultiplyNumber Test:");
            Console.WriteLine($"Vector1: {vector1}");
            Console.WriteLine($"Multiply of Vector1 * 2 = {result}");
        }
        
        
        public void SumTest()
        {
            try
            {
                var vectorNew1 = new MathVector(1, 2, 3);
                var vectorNew2 = new MathVector(3, 4, 5);
                var vectorNew3 = new MathVector(6, 7, 8, 9);
            
                var result = vectorNew1.Sum(vectorNew2);
            
                Console.WriteLine("\nSum Test:");
                Console.WriteLine($"Vector1: {vectorNew1}");
                Console.WriteLine($"Vector2: {vectorNew2}");
                Console.WriteLine($"Vector1 + Vector2 = {result}");
                Console.WriteLine($"Vector1 + Vector3 = {vectorNew1.Sum(vectorNew3)}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
        
        public void MultiplyTest()
        {
            try
            {
                var vectorNew1 = new MathVector(2, 3, 4);
                var vectorNew2 = new MathVector(1, 2, 3);
                var vectorNew3 = new MathVector(6, 7, 8, 9);
            
                var result = vectorNew1.Multiply(vectorNew2);
            
                Console.WriteLine("\nMultiply Test:");
                Console.WriteLine($"Vector1: {vectorNew1}");
                Console.WriteLine($"Vector2: {vectorNew2}");
                Console.WriteLine($"Vector1 * Vector2 = {result}");
                Console.WriteLine($"Vector1 + Vector3 = {vectorNew1.Sum(vectorNew3)}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
        
        
        public void ScalarMultiplyTest()
        {
            try
            {
                var vectorNew1 = new MathVector(2, 3);
                var vectorNew2 = new MathVector(4, 5);
                var vectorNew3 = new MathVector(6, 7, 8, 9);
            
                var result = vectorNew1.ScalarMultiply(vectorNew2);
            
                Console.WriteLine("\nScalarMultiply Test:");
                Console.WriteLine($"Vector1: {vectorNew1}");
                Console.WriteLine($"Vector2: {vectorNew2}");
                Console.WriteLine($"Vector1 * Vector2 = {result}");
                Console.WriteLine($"Vector1 * Vector3 = {vectorNew1.ScalarMultiply(vectorNew3)}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
        
        
        public void CalcDistanceTest()
        {
            try
            {
                var vectorNew1 = new MathVector(2, 3);
                var vectorNew2 = new MathVector(4, 5);
                var vectorNew3 = new MathVector(6, 7, 8, 9);
            
                var result = vectorNew1.CalcDistance(vectorNew2);
            
                Console.WriteLine("\nCalcDistance Test:");
                Console.WriteLine($"Vector1: {vectorNew1}");
                Console.WriteLine($"Vector2: {vectorNew2}");
                Console.WriteLine($"Vector1 and Vector2 = {result}");
                Console.WriteLine($"Vector1 and Vector2 = {vectorNew1.CalcDistance(vectorNew3)}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}