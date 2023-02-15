using System.Diagnostics;
using Lib.Selection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs
{
    [TestClass]
    public class TestPeekFindingTest
    {
        public static IEnumerable<int> RandomList(int length)
        {
            Random rand = new Random();
            return Enumerable.Range(0, length)
                    .Select(i => new Tuple<int, int>(rand.Next(length), i))
                    .OrderBy(i => i.Item1)
                    .Select(i => i.Item2);
        }
        [TestMethod]
        public void Test_Random()
        {
            var sortedList = RandomList(10000000).OrderBy(x => x).Concat(new List<int>{1});
            var input = sortedList.Concat(sortedList).Concat(sortedList).ToArray();
            Stopwatch sw = new Stopwatch();
            Debug.WriteLine("control begin");
            sw.Reset();
            sw.Start();
            var control = PeekFinding.PeekAll<int>(input, input.Length);
            sw.Stop();
            Debug.WriteLine("control end");
            Debug.WriteLine($"Elapsed: {sw.Elapsed.TotalMilliseconds}");
            
            Debug.WriteLine("PeekAny begin");
            sw.Reset();
            sw.Start();
            var peakAnyResult = PeekFinding.PeekAny<int>(input);
            sw.Stop();
            Debug.WriteLine("PeekAny end");
            var peakAnyTime = sw.Elapsed.TotalMilliseconds;
            Debug.WriteLine($"Elapsed: {sw.Elapsed.TotalMilliseconds}");
            Debug.WriteLine($"PeekAny: {peakAnyResult}");
            Assert.IsTrue(control.Contains(peakAnyResult));
            

            Debug.WriteLine("PeekAnyLinear begin");
            sw.Reset();
            sw.Start();
            var peekAnyLinearResult = PeekFinding.PeekAnyLinear<int>(input, input.Length);
            sw.Stop();
            Debug.WriteLine("PeekAnyLinear end");
            Assert.IsTrue(control.Contains(peekAnyLinearResult));
            var peekAnyLinearTime = sw.Elapsed.TotalMilliseconds;
            Debug.WriteLine($"Elapsed: {sw.Elapsed.TotalMilliseconds}");
            Debug.WriteLine($"PeekAnyLinear: {peekAnyLinearTime}");
            Assert.IsTrue(peakAnyTime < peekAnyLinearTime);

        }
        [TestMethod]
        [DataRow(new int[] { 5,4,3,2,1 }, 0)]
        [DataRow(new int[] { 1,5,4,3,2 }, 1)]
        [DataRow(new int[] { 1,2,5,4,3 }, 2)]
        [DataRow(new int[] { 1,2,3,5,4 }, 3)]
        [DataRow(new int[] { 1,2,3,4,5 }, 4)]
        [DataRow(new int[] { 5,4,3,2,1,0 }, 0)]
        [DataRow(new int[] { 1,5,4,3,2,1 }, 1)]
        [DataRow(new int[] { 1,2,5,4,3,2 }, 2)]
        [DataRow(new int[] { 1,2,3,5,4,3 }, 3)]
        [DataRow(new int[] { 1,2,3,4,5,4 }, 4)]
        [DataRow(new int[] { 1,2,3,4,5,6 }, 5)]
        [DataRow(new int[] { 10, 20, 15, 2, 23, 90, 67}, 1)]
        [DataRow(new int[] { 6,1,3,0,4,5,8,9,2,7}, 7)]
        public void PeekAny_Test(int[] input, int answer)
        {
            var result = PeekFinding.PeekAny<int>(input);
            Debug.WriteLine(result);
            Assert.AreEqual(answer, result);
        }

        [TestMethod]
        [DataRow(new int[] { 5,4,3,2,1 }, 0)]
        [DataRow(new int[] { 1,5,4,3,2 }, 1)]
        [DataRow(new int[] { 1,2,5,4,3 }, 2)]
        [DataRow(new int[] { 1,2,3,5,4 }, 3)]
        [DataRow(new int[] { 1,2,3,4,5 }, 4)]
        [DataRow(new int[] { 5,4,3,2,1,0 }, 0)]
        [DataRow(new int[] { 1,5,4,3,2,1 }, 1)]
        [DataRow(new int[] { 1,2,5,4,3,2 }, 2)]
        [DataRow(new int[] { 1,2,3,5,4,3 }, 3)]
        [DataRow(new int[] { 1,2,3,4,5,4 }, 4)]
        [DataRow(new int[] { 1,2,3,4,5,6 }, 5)]
        [DataRow(new int[] { 10, 20, 15, 2, 23, 90, 67}, 1)]
        [DataRow(new int[] { 6,1,3,0,4,5,8,9,2,7}, 7)]
        public void PeekAll_Test(int[] input, int answer)
        {
            var control = PeekFinding.PeekAll<int>(input, input.Length);

            Assert.IsTrue(control.Contains(answer));
        }
    }
}