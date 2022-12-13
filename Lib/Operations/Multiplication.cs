using System;
using System.Collections.Generic;
using System.Numerics;
using Extensions;

namespace Lib.Operations
{

    public class Multiplication
    {
        public BigInteger Multiply(KeyValuePair<string,string> input) => this.Memoized(input, x =>
        {
            string inputA = input.Key;
            string inputB = input.Value;
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
            var ac = Multiply(new KeyValuePair<string,string>(a,c));
            var bd = Multiply(new KeyValuePair<string,string>(b,d));
            var pq = Multiply(new KeyValuePair<string,string>(p,q));
            var adbc = pq - ac - bd;
            var result = (BigInteger.Pow(10,inputA.Length) * ac) + 
                         (BigInteger.Pow(10,inputA.Length/2) * adbc) +
                         bd;
            return (BigInteger)result;
        });
    }
}