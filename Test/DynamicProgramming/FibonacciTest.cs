using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Model;
using Extensions;

namespace Test.DynamicProgramming
{
    
    [TestClass]
    public class FibonacciTest
    {
        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        [DataRow(10, 89)]
        [DataRow(45, 1836311903)]
        public void FibonacciSlow_Test(int n, int expectedResult)
        {
            var result = FibonacciSlow(n);
            Assert.AreEqual(expectedResult, result);
        }

        public int FibonacciSlow(int n)
        {
            if(n <= 2){
                return n;
            }
            return FibonacciSlow(n - 1) + FibonacciSlow(n - 2);
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        [DataRow(10, 89)]
        [DataRow(45, 1836311903)]
        [DataRow(100, 1298777728820984005)]
        public void Fibonacci_Test(int n, int expectedResult)
        {
            var result = Fibonacci(n);
            Assert.AreEqual(expectedResult, result);
        }
        public long Fibonacci(int n)
        {
            var cache = new List<long>();
            cache.Add(1);
            cache.Add(2);
            for(var i=2;i<n;i++){
                cache.Add(cache[i-1] + cache[i-2]);
            }
            return cache[n-1];
        }
    }
}
