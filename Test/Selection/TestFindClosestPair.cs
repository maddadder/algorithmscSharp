using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using Lib.Selection;
using Lib.Model;

namespace Test
{
    [TestClass]
    public class TestFindSmallestLineSegment
    {
        public static float[] RandomList(int length)
        {
            Random rand = new Random();
            return Enumerable.Range(0, length)
                    .Select(i => (float)rand.NextDouble()).ToArray();
        }
        public static Vector2[] ToPoint2d(float[] x, float[] y)
        {
            var len = x.Length;
            if(len!=y.Length)
                throw new InvalidOperationException("input size must match");
            Vector2[] output = new Vector2[len];
            for(var i=0;i<len;i++)
            {
                output[i] = new Vector2(x[i],y[i]);
            }
            return output;
        }
        [TestMethod]
        public void Test_FindSmallestLineSegment()
        {
            var inputX = RandomList((int)Math.Pow(10,4));
            var inputY = RandomList((int)Math.Pow(10,4));
            var input = ToPoint2d(inputX, inputY);
            var inputControlX = new float[inputX.Length];
            var inputControlY = new float[inputY.Length];
            Array.Copy(inputX, inputControlX, inputX.Length);
            Array.Copy(inputY, inputControlY, inputY.Length);
            var inputControl = ToPoint2d(inputControlX, inputControlY);
            Stopwatch sw = new Stopwatch();
            Debug.WriteLine("test begin");
            sw.Start();
            var test = SmallestLineSegment.FindSmallestLineSegment(input);
            sw.Stop();
            var testDuration = sw.Elapsed;
            Debug.WriteLine("test end");
            Debug.WriteLine(sw.ElapsedMilliseconds);
            Debug.WriteLine("control begin");
            sw.Reset();
            sw.Start();
            var control = SmallestLineSegment.FindSmallestLineSegmentBruteForce(inputControl);
            sw.Stop();
            var controlDuration = sw.Elapsed;
            Debug.WriteLine("control end");
            Debug.WriteLine(sw.ElapsedMilliseconds);
            var testDistance = test.DistanceSquared();
            var controlDistance = control.DistanceSquared();
            Assert.AreEqual(testDistance,controlDistance);
            
            if(testDuration < controlDuration){
                Debug.WriteLine("Test Wins");
            }
            else{
                Debug.WriteLine("Control Wins");
            }
        }
        
    }
}
