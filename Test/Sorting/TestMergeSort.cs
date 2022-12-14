using System;
using System.Diagnostics;
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
        public static IEnumerable<int> Control_Sort( IEnumerable<int> input )
        {
            return input.OrderBy(p => p).ToArray();
        }

        [TestMethod]
        public void Test_MergeSort()
        {
            var input = RandomList((int)Math.Pow(10,7));
            var inputControl = new int[input.Length];
            Array.Copy(input, inputControl, input.Length);
            Stopwatch sw = new Stopwatch();
            Debug.WriteLine("test begin");
            sw.Start();
            var test = ListOperations.MergeSort(input);
            var reverse = ListOperations.Reverse<int>(test);
            sw.Stop();
            var testDuration = sw.Elapsed;
            Debug.WriteLine("test end");
            Debug.WriteLine(sw.ElapsedMilliseconds);
            Debug.WriteLine("control begin");
            sw.Reset();
            sw.Start();
            var control = Control_Sort(inputControl);
            var control_reverse = control.Reverse<int>();
            sw.Stop();
            var controlDuration = sw.Elapsed;
            Debug.WriteLine("control end");
            Debug.WriteLine(sw.ElapsedMilliseconds);
            var testList = test.ToArray();
            var controlList = control.ToArray();
            for(var i=0;i<control.Count();i++){
                Assert.AreEqual(testList[i], controlList[i]);
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
