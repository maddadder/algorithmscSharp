using System;
using System.Diagnostics;
using Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.DynamicProgramming
{
    
    [TestClass]
    public class MaximumWeightedIndependentSetTest
    {

        [TestMethod]
        [DataRow(new int[] { 1, 5 }, 5, new int[] { 1 })]
        [DataRow(new int[] { 5, 1 }, 5, new int[] { 0 })]
        public void CalculateMaximumWeightedIndependentSet_TwoNodes(int[] nodes, long expectedResult, int[] expectedUsedNodeIds)
        {
            Graph graph = new Graph(nodes);

            long actualResult = graph.CalculateMWIS();
            List<int> actualUsedNodeIDs = graph.UsedNodeIds.ToList();

            Assert.AreEqual(expectedResult, actualResult);
            for(var i = 0;i<expectedUsedNodeIds.Length;i++)
                Assert.AreEqual(expectedUsedNodeIds.ToList()[i], actualUsedNodeIDs[i]);
        }

        [TestMethod]
        [DataRow(new int[] { 1, 5, 2 }, 5, new int[] { 1 })]
        [DataRow(new int[] { 5, 3, 1 }, 6, new int[] { 2, 0 })]
        public void CalculateMaximumWeightedIndependentSet_ThreeNodes(int[] nodes, long expectedResult, int[] expectedUsedNodeIds)
        {
            Graph graph = new Graph(nodes);

            long actualResult = graph.CalculateMWIS();
            List<int> actualUsedNodeIDs = graph.UsedNodeIds.ToList();

            Assert.AreEqual(expectedResult, actualResult);
            for(var i = 0;i<expectedUsedNodeIds.Length;i++)
                Assert.AreEqual(expectedUsedNodeIds.ToList()[i], actualUsedNodeIDs[i]);
        }

        [TestMethod]
        [DataRow(new int[] { 1, 5, 2, 4 }, 9, new int[] { 3, 1 })]
        [DataRow(new int[] { 5, 3, 4, 1 }, 9, new int[] { 2, 0 })]
        [DataRow(new int[] { 5, 3, 1, 4 }, 9, new int[] { 3, 0 })]
        public void CalculateMaximumWeightedIndependentSet_FourNodes(int[] nodes, long expectedResult, int[] expectedUsedNodeIds)
        {
            Graph graph = new Graph(nodes);

            long actualResult = graph.CalculateMWIS();
            List<int> actualUsedNodeIDs = graph.UsedNodeIds.ToList();

            Assert.AreEqual(expectedResult, actualResult);
            for(var i = 0;i<expectedUsedNodeIds.Length;i++)
                Assert.AreEqual(expectedUsedNodeIds.ToList()[i], actualUsedNodeIDs[i]);
        }
        [TestMethod]
        [DataRow(new int[] { 3, 2, 1, 6, 4, 5 }, 14, new int[] { 5, 3, 0 })]
        public void CalculateMaximumWeightedIndependentSet_SixNodes(int[] nodes, long expectedResult, int[] expectedUsedNodeIds)
        {
            Graph graph = new Graph(nodes);

            long actualResult = graph.CalculateMWIS();
            List<int> actualUsedNodeIDs = graph.UsedNodeIds.ToList();

            Assert.AreEqual(expectedResult, actualResult);
            for(var i = 0;i<expectedUsedNodeIds.Length;i++)
                Assert.AreEqual(expectedUsedNodeIds.ToList()[i], actualUsedNodeIDs[i]);
        }
        [TestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 30, new int[] { 9, 7, 5, 3, 1 })]
        public void CalculateMaximumWeightedIndependentSet_TenNodes(int[] nodes, long expectedResult, int[] expectedUsedNodeIds)
        {
            Graph graph = new Graph(nodes);

            long actualResult = graph.CalculateMWIS();
            List<int> actualUsedNodeIDs = graph.UsedNodeIds.ToList();

            Assert.AreEqual(expectedResult, actualResult);
            for(var i = 0;i<expectedUsedNodeIds.Length;i++)
                Assert.AreEqual(expectedUsedNodeIds.ToList()[i], actualUsedNodeIDs[i]);
        }
        [TestMethod]
        public void CalculateMaximumWeightedIndependentSet_CourseraAssignment()
        {
            string sourceFile = "../../../mwis.txt";

            Graph graph = new Graph(sourceFile);

            graph.CalculateMWIS();

            List<int> nodeIdsToCheck = new List<int>() { 0, 1, 2, 3, 16, 116, 516, 996 };

            string output="";

            foreach (var nodeId in nodeIdsToCheck)
            {
                if (graph.UsedNodeIds.Contains(nodeId))
                {
                    output += "1";
                }
                else
                {
                    output += "0";
                }
            }

            Debug.WriteLine(output);
        }
        
    }
    public class Graph
    {
        public IReadOnlyCollection<int> UsedNodeIds => usedNodeIds;

        int[] nodes;
        long[] results;
        List<int> usedNodeIds = new List<int>();

        public Graph(int[] nodes)
        {
            this.nodes = nodes;
        }

        public Graph(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int numberOfNodes = int.Parse(lines[0]);
            nodes = new int[numberOfNodes];
            for (int i = 1; i < lines.Length; i++)
            {
                int weigth = int.Parse(lines[i]);
                nodes[i - 1] = weigth;
            }
        }
        private void CalculateUsedNodeIDs()
        {
            int nodeIndex = nodes.Length - 1;
            while (nodeIndex >= 1)
            {
                if (nodes[nodeIndex] + results[nodeIndex - 1] > results[nodeIndex])
                {
                    usedNodeIds.Add(nodeIndex);
                    nodeIndex -= 2;
                }
                else
                {
                    nodeIndex--;
                }
            }
            if (nodeIndex == 0)
            {
                usedNodeIds.Add(nodeIndex);
            }
        }

        public long CalculateMWIS(bool useRecursion = false)
        {
            results = new long[nodes.Length + 1];
            results[0] = 0;
            results[1] = nodes[0];
            if(useRecursion){
                var result = CalculateMWIS(nodes.Length);
                CalculateUsedNodeIDs();
                return result;
            }
            for(var i = 2;i<=nodes.Length;i++){
                results[i] = Math.Max(results[i-1], results[i-2] + nodes[i-1]);
            }
            CalculateUsedNodeIDs();
            return results[nodes.Length];
        }
        private long CalculateMWIS(long n) => this.Memoized(n, x =>
        {
            if (n == 0)
            {
                return 0;
            }
            else if (n == 1){
                return nodes[n-1];
            }
            else
            {
                var s1 = CalculateMWIS(n - 1);
                var s2 = CalculateMWIS(n - 2);
                results[n] = Math.Max(s1, s2 + nodes[n-1]);
                return results[n];
            }
        });
    }
}
