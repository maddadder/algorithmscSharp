using Lib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs
{
    [TestClass]
    public class TestMathGraphPrimsMST
    {
        [TestMethod]
        public void FindMinimumSpanningTreeTotalCost_TwoNodes_ReturnsCost()
        {
            MathGraph<int> mst = new MathGraph<int>();
            
            mst.AddEdge(0,1,4);
            int source = 0;
            mst.prims_mst(source);
            

            var expectedCost = 4;

            // Act
            var actualCost = mst.print_distance(source);

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }

        [TestMethod]
        public void FindMinimumSpanningTreeTotalCost_ThreeNodesSameEdgeLength_ReturnsCost()
        {
            // Arrange
            
            MathGraph<int> mst = new MathGraph<int>();
            mst.AddEdge(0,1,4);
            mst.AddEdge(1,2,4);
            int source = 0;
            mst.prims_mst(source);

            var expectedCost = 8;

            // Act
            var actualCost = mst.print_distance(source);

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }

        [TestMethod]
        public void FindMinimumSpanningTreeTotalCost_ThreeNodesDifferentEdgeLength_ReturnsCost()
        {
            // Arrange
            MathGraph<int> mst = new MathGraph<int>();
            mst.AddEdge(0,1,4);
            mst.AddEdge(1,2,3);
            int source = 0;
            mst.prims_mst(source);

            var expectedCost = 7;

            // Act
            var actualCost = mst.print_distance(source);

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }

        [TestMethod]
        public void FindMinimumSpanningTreeTotalCost_FourNodesDifferentEdgeLength_ReturnsCost()
        {
            // Arrange
            MathGraph<int> mst = new MathGraph<int>();

            mst.AddEdge(0,1,4);
            mst.AddEdge(1,3,1);
            mst.AddEdge(0,2,3);
            mst.AddEdge(1,2,5);
            mst.AddEdge(2,3,2);
            int source = 0;
            mst.prims_mst(source);

            var expectedCost = 6;

            // Act
            var actualCost = mst.print_distance(source);

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
            
        }

        [TestMethod]
        public void FindMinimumSpanningTreeTotalCost_ComplexTree_ReturnsCost()
        {
            // Arrange
            MathGraph<int> mst = new MathGraph<int>();
            mst.AddEdge(7,6,1);
            mst.AddEdge(8,2,2);
            mst.AddEdge(6,5,2);
            mst.AddEdge(0,1,4);
            mst.AddEdge(2,5,4);
            mst.AddEdge(8,6,6);
            mst.AddEdge(2,3,7);
            mst.AddEdge(7,8,7);
            mst.AddEdge(0,7,8);
            mst.AddEdge(1,2,8);
            mst.AddEdge(3,4,9);
            mst.AddEdge(5,4,10);
            mst.AddEdge(1,7,11);
            mst.AddEdge(0,1,4);
            mst.AddEdge(3,5,14);
            int source = 0;
            mst.prims_mst(source);


            var expectedCost = 37;

            // Act
            var actualCost = mst.print_distance(source);

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }

        [TestMethod]
        public void Test_Simple()
        {
            string sourceFile = "../../../MST1.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> pmst = new MathGraph<int>();
            
            var expectedCost = 5;

            // Act
            var actualCost = MathGraph<int>.managePrimsMST(lines);

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }
        [TestMethod]
        public void TestIn2()
        {
            string sourceFile = "../../../MST2.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> pmst = new MathGraph<int>();
            
            var expectedCost = -3612829;

            // Act
            var actualCost = MathGraph<int>.managePrimsMST(lines);

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }
        [TestMethod]
        public void TestIn3()
        {
            string sourceFile = "../../../MST3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> pmst = new MathGraph<int>();
            
            var expectedCost = 14;

            // Act
            var actualCost = MathGraph<int>.managePrimsMST(lines);
        
            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }
        [TestMethod]
        public void TestIn4()
        {
            string sourceFile = "../../../MST4.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> pmst = new MathGraph<int>();

            var expectedCost = 4;

            // Act
            var actualCost = MathGraph<int>.managePrimsMST(lines);;
        
            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }
    }
}