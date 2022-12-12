using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Sorting;
using Lib.Operations;
using System.Numerics;

namespace Test
{
    [TestClass]
    public class TestMultiplication
    {
        private static Random random = new Random();
        private static BigInteger Control( string inputA, string inputB )
        {
            return (BigInteger.Parse(inputA) * BigInteger.Parse(inputB));
        }

        [TestMethod]
        public void Test_TestMultiplication()
        {
            string inputA = "1234567890";
            string inputB = "1234567890";
            int iterations = 13;
            Stopwatch sw = new Stopwatch();
            Debug.WriteLine("test begin");
            sw.Start();
            Multiplication obj = new Multiplication();
            BigInteger test = obj.Multiply(new KeyValuePair<string,string>(inputA, inputB));
            for(var i = 0;i<iterations;i++)
            {
                Debug.WriteLine(test.ToString().Length + " Digits");
                test = obj.Multiply(new KeyValuePair<string,string>(test.ToString(), test.ToString()));
            }
            sw.Stop();
            var testDuration = sw.Elapsed;
            Debug.WriteLine("test end");
            Debug.WriteLine(sw.Elapsed.Ticks);
            Debug.WriteLine("control begin");
            sw.Reset();
            sw.Start();
            BigInteger control = Control(inputA, inputB);
            for(var i = 0;i<iterations;i++)
            {
                control = Control(control.ToString(), control.ToString());
            }
            sw.Stop();
            var controlDuration = sw.Elapsed;
            Debug.WriteLine("control end");
            Debug.WriteLine(sw.Elapsed.Ticks);
            Assert.AreEqual(test, control);
            if(testDuration < controlDuration){
                Debug.WriteLine("Test Wins");
            }
            else{
                Debug.WriteLine("Control Wins");
            }
            
        }
    }
}
