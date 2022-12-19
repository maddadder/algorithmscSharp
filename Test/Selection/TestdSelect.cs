using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Sorting;
using Lib.Selection;

namespace Test
{
    [TestClass]
    public class TestdSelect
    {
        public static int[] RandomList(int length)
        {
            Random rand = new Random();
            return Enumerable.Range(0, length)
                    .Select(i => new Tuple<int, int>(rand.Next(length), i))
                    .OrderBy(i => i.Item1)
                    .Select(i => i.Item2).ToArray();
        }
        public static IEnumerable<T> Control_Sort<T>( IEnumerable<T> input ) where T : IComparable
        {
            return input.OrderBy(p => p).ToArray();
        }
        public static List<string> ListOfStrings(int len, int strLen)
        {
            List<string> Items = new List<string>(len);
            for(var i = 0;i<len;i++)
            {
                var s = TestReverseSort.RandomString(strLen);
                Items.Add(s);
            }
            return Items;
        }
        [TestMethod]
        public void Test_dSelect()
        {
            int index = 50;
            //var input = TestReverseSort.RandomString((int)Math.Pow(10, 2)).ToArray().Distinct().ToArray();
            //var input = ListOfStrings((int)Math.Pow(10,1)).ToArray();
            var input = RandomList((int)Math.Pow(10,6));
            var inputControl = new int[input.Length];
            Array.Copy(input, inputControl, input.Length);
            Stopwatch sw = new Stopwatch();
            Debug.WriteLine("test begin");
            sw.Start();
            var test = dSelect.FindNthSmallestNumber(input,index);
            sw.Stop();
            var testDuration = sw.Elapsed;
            Debug.WriteLine("test end");
            Debug.WriteLine(sw.ElapsedMilliseconds);
            Debug.WriteLine("control begin");
            sw.Reset();
            sw.Start();
            var control = Control_Sort(inputControl);
            sw.Stop();
            var controlDuration = sw.Elapsed;
            Debug.WriteLine("control end");
            Debug.WriteLine(sw.ElapsedMilliseconds);

            var controlList = control.ToArray();
            Assert.AreEqual(controlList[index], test);
            
            if(testDuration < controlDuration){
                Debug.WriteLine("Test Wins");
            }
            else{
                Debug.WriteLine("Control Wins");
            }
        }
        
    }
}
