using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;

namespace MathLibrary
{
    public class BaseClass
    {
        public int GetFactors(int number, out List<Tuple<int,int>> factors)
        {
            int totalFactors = 0;
            int sqrt = (int)Math.Ceiling(Math.Sqrt(number));
            int _number = number;
            factors = new List<Tuple<int, int>>();
            for(int i = 1; i < sqrt; ++i)
            {
                // if a pair of factors
                if(number % i == 0)
                {
                    totalFactors += 2;
                    factors.Add(new Tuple<int, int>(i, number / i));
                }
            }

            // the perfect square is also a factor
            if(sqrt * sqrt == number)
            {
                ++totalFactors;
                factors.Add(new Tuple<int, int>(sqrt, sqrt));
            }

            return totalFactors;
        }

        public int CountDigits(int number)
        {
            int totalDigits = 1;
            while(number > 0)
            {
                number /= 10;
                ++totalDigits;
            }
            return totalDigits;
        }

        public double functionX(int x)
        {
            return x * x + 1;
        }

    }
}
