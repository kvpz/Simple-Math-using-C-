using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathLibrary;
using System.Collections.Generic;
using System.Diagnostics;

namespace MathLibrary.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetFactorsTest()
        {
            BaseClass baseclass = new BaseClass();
            List<Tuple<int, int>> factors;
            baseclass.GetFactors(64, out factors);
            foreach(Tuple<int,int> t in factors)
            {
                Console.WriteLine($"{t.Item1} & {t.Item2}");
            }
        }

        [TestMethod]
        public void TestFunction()
        {
            List<int> domain = new List<int>() { -2, -1, 0, 1, 3 };
            BaseClass baseClass = new BaseClass();

            foreach(int x in domain)
            {
                Console.WriteLine(baseClass.functionX(x));
            }
        }
    }
}
