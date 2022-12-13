using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Sorting;
namespace Test
{
    [TestClass]
    public class TestReverseSort
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string Control_Reverse_Sort( string s )
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse( charArray );
            return new string( charArray );
        }

        [TestMethod]
        public void Test_Reverse_Sort()
        {
            string input = RandomString((int)Math.Pow(10, 8));
            Stopwatch sw = new Stopwatch();
            Debug.WriteLine("test begin");
            sw.Start();
            string test = ListOperations.Reverse(input);
            sw.Stop();
            var testDuration = sw.Elapsed;
            Debug.WriteLine("test end");
            Debug.WriteLine(sw.Elapsed.Ticks);
            Debug.WriteLine("control begin");
            sw.Reset();
            sw.Start();
            string control = Control_Reverse_Sort(input);
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
