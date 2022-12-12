using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Lib.Operations
{

    public static class Multiplication
    {
        
        public static BigInteger Multiply(string inputA, string inputB)
        {
            if(inputA.Length == 1 && inputB.Length == 1)
                return (System.Numerics.BigInteger.Parse(inputA) * BigInteger.Parse(inputB));

            while(inputA.Length < inputB.Length || inputA.Length % 2 != 0)
                inputA = "0" + inputA;
            while(inputB.Length < inputA.Length || inputB.Length % 2 != 0)
                inputB = "0" + inputB;
            
            if(inputA.Length % 2 != 0 || inputB.Length % 2 != 0)
                throw new InvalidOperationException("number of digits must be even");

            if(inputA.Length != inputB.Length)
                throw new InvalidOperationException("number of digits of a must match b");

            var a = inputA.Substring(0,inputA.Length / 2);
            var b = inputA.Substring(inputA.Length / 2, inputA.Length / 2);
            var c = inputB.Substring(0,inputB.Length / 2);
            var d = inputB.Substring(inputB.Length / 2, inputB.Length / 2);
            var p = (BigInteger.Parse(a)+BigInteger.Parse(b)).ToString();
            var q = (BigInteger.Parse(c)+BigInteger.Parse(d)).ToString();
            var ac = Multiply(a,c);
            var bd = Multiply(b,d);
            var pq = Multiply(p,q);
            var adbc = pq - ac - bd;
            var result = (BigInteger.Pow(10,inputA.Length) * ac) + 
                         (BigInteger.Pow(10,inputA.Length/2) * adbc) +
                         bd;
            return (BigInteger)result;
        }
    }
}