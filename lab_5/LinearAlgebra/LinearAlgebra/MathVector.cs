using System.Collections;

namespace LinearAlgebra
{
    public class MathVector : IMathVector
    {
        private List<double> _components = new List<double>();
        
        
        public MathVector(params double[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values), "There is no index of such a vector!");
            _components.AddRange(values);
        }
        

        public int Dimensions
        {
            get
            {
                var count = _components.Count;
                if (count < 0)
                {
                    throw new InvalidOperationException("Dimensions cannot be negative!");
                }
                return count;
            }
        }

        
        public double this[int i]
        {
            get
            {
                if (i < 0 || i >= Dimensions)
                    throw new IndexOutOfRangeException("The index is out of the acceptable range!");
                return _components[i];
            }
            set
            {
                if (i < 0 || i >= Dimensions)
                    throw new IndexOutOfRangeException("The index is out of the acceptable range!");
                _components[i] = value;
            }
        }
        

        public double Length => Math.Sqrt(_components.Sum(x => x * x));
        

        public IMathVector SumNumber(double number)
        {
            return new MathVector(_components.Select(x => x + number).ToArray());
        }
        

        public IMathVector MultiplyNumber(double number)
        {
            return new MathVector(_components.Select(x => x * number).ToArray());
        }
        

        public IMathVector Sum(IMathVector vector)
        {
            if (vector.Dimensions != Dimensions)
                throw new InvalidOperationException("Vectors must have the same dimensions!");

            var result = new MathVector(_components.ToArray());
            for (var i = 0; i < Dimensions; i++)
            {
                result[i] += vector[i];
            }

            return result;
        }
        

        public IMathVector Multiply(IMathVector vector)
        {
            if (vector.Dimensions != Dimensions)
                throw new InvalidOperationException("Vectors must have the same dimensions!");

            var result = new MathVector(_components.ToArray());
            for (var i = 0; i < Dimensions; i++)
            {
                result[i] *= vector[i];
            }

            return result;
        }
        

        public double ScalarMultiply(IMathVector vector)
        {
            if (vector.Dimensions != Dimensions)
                throw new InvalidOperationException("Vectors must have the same dimensions!");

            double result = 0;
            for (var i = 0; i < Dimensions; i++)
            {
                result += this[i] * vector[i];
            }

            return result;
        }
        

        public double CalcDistance(IMathVector vector)
        {
            if (vector.Dimensions != Dimensions)
                throw new InvalidOperationException("Vectors must have the same dimensions!");

            double result = 0;
            for (var i = 0; i < Dimensions; i++)
            {
                result += Math.Pow(this[i] - vector[i], 2);
            }

            return Math.Sqrt(result);
        }
        

        public IEnumerator GetEnumerator()
        {
            return _components.GetEnumerator();
        }
        

        public static IMathVector operator +(MathVector vector1, MathVector vector2)
        {
            return vector1.Sum(vector2);
        }
        

        public static IMathVector operator +(MathVector vector1, double number)
        {
            return vector1.SumNumber(number);
        }
        

        public static IMathVector operator -(MathVector vector1, MathVector vector2)
        {
            return vector1.Sum(vector2.MultiplyNumber(-1));
        }
        

        public static IMathVector operator -(MathVector vector1, double number)
        {
            return vector1.MultiplyNumber(-number);
        }
        

        public static IMathVector operator *(MathVector vector1, MathVector vector2)
        {
            return vector1.Multiply(vector2);
        }
        

        public static IMathVector operator *(MathVector vector1, double number)
        {
            return vector1.MultiplyNumber(number);
        }
        

        public static IMathVector operator /(MathVector vector1, MathVector vector2)
        {
            var newVector = new MathVector(vector2._components.ToArray());

            for (var i = 0; i < vector2.Dimensions; i++)
            {
                newVector[i] = Math.Pow(vector2[i], -1);
            }
            return vector1.Multiply(newVector);
        }


        public static IMathVector operator /(MathVector vector1, double number)
        {
            return vector1.MultiplyNumber(1 / number);
        }
        

        public static double operator %(MathVector vector1, MathVector vector2)
        {
            return vector1.ScalarMultiply(vector2);
        }
        

        public override string ToString()
        {
            return string.Join(", ", _components);
        }
    }
}
