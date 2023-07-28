using System.Diagnostics;
using Lib.Selection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs
{
    [TestClass]
    public class TestPeekFinding2dTest
    {

        [TestMethod]
        public void PeekAny_Test()
        {
            int[,] input = new int[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9,10,11,12 }, { 13,14,15,16 } };
            int answer = 16;
            var result = PeekFinding2d.PeekAny<int>(input);
            Debug.WriteLine(result);
            Assert.AreEqual(answer, input[result[0],result[1]]);
        }

    }
}