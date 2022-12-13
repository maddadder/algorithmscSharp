using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Sorting;
namespace Test
{
    [TestClass]
    public class TestMergeSort
    {
        public static int[] RandomList(int length)
        {
            Random rand = new Random();
            return Enumerable.Range(0, length)
                    .Select(i => new Tuple<int, int>(rand.Next(length), i))
                    .OrderBy(i => i.Item1)
                    .Select(i => i.Item2).ToArray();
        }
        public static int[] Control_Sort( int[] input )
        {
            return input.OrderBy(p => p).ToArray();
        }

        [TestMethod]
        public void Test_MergeSort()
        {
            int[] input = RandomList((int)Math.Pow(10,7)).ToArray();
            Stopwatch sw = new Stopwatch();
            Debug.WriteLine("test begin");
            sw.Start();
            int[] test = ListOperations.MergeSort(input);
            sw.Stop();
            var testDuration = sw.Elapsed;
            Debug.WriteLine("test end");
            Debug.WriteLine(sw.ElapsedMilliseconds);
            Debug.WriteLine("control begin");
            sw.Reset();
            sw.Start();
            int[] control = Control_Sort(input);
            sw.Stop();
            var controlDuration = sw.Elapsed;
            Debug.WriteLine("control end");
            Debug.WriteLine(sw.ElapsedMilliseconds);
            for(var i=0;i<control.Length;i++){
                Assert.AreEqual(test[i], control[i]);
            }
            
            if(testDuration < controlDuration){
                Debug.WriteLine("Test Wins");
            }
            else{
                Debug.WriteLine("Control Wins");
            }
        }
        
    }
}
