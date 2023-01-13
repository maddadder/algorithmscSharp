using Lib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs
{
    [TestClass]
    public class TestPrimsMST
    {
        [TestMethod]
        public void FindMinimumSpanningTreeTotalCost_TwoNodes_ReturnsCost()
        {
            int nodes = 2;
            int edges = 1;
            PrimsMST mst = new PrimsMST();
            mst.initPrimsMST(nodes, edges);
            mst.AddNode(0,1,4);
            int source = 0;
            mst.prims_mst(source);
            

            decimal expectedCost = 4;

            // Act
            decimal actualCost = mst.print_distance(source);

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }

        [TestMethod]
        public void FindMinimumSpanningTreeTotalCost_ThreeNodesSameEdgeLength_ReturnsCost()
        {
            // Arrange
            
            int nodes = 3;
            int edges = 1;
            PrimsMST mst = new PrimsMST();
            mst.initPrimsMST(nodes, edges);
            mst.AddNode(0,1,4);
            mst.AddNode(1,2,4);
            int source = 0;
            mst.prims_mst(source);

            decimal expectedCost = 8;

            // Act
            decimal actualCost = mst.print_distance(source);

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }

        [TestMethod]
        public void FindMinimumSpanningTreeTotalCost_ThreeNodesDifferentEdgeLength_ReturnsCost()
        {
            // Arrange
            int nodes = 3;
            int edges = 1;
            PrimsMST mst = new PrimsMST();
            mst.initPrimsMST(nodes, edges);
            mst.AddNode(0,1,4);
            mst.AddNode(1,2,3);
            int source = 0;
            mst.prims_mst(source);

            decimal expectedCost = 7;

            // Act
            decimal actualCost = mst.print_distance(source);

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }

        [TestMethod]
        public void FindMinimumSpanningTreeTotalCost_FourNodesDifferentEdgeLength_ReturnsCost()
        {
            // Arrange
            int nodes = 4;
            int edges = 5;
            PrimsMST mst = new PrimsMST();
            mst.initPrimsMST(nodes, edges);
            mst.AddNode(0,1,4);
            mst.AddNode(1,3,1);
            mst.AddNode(0,2,3);
            mst.AddNode(1,2,5);
            mst.AddNode(2,3,2);
            int source = 0;
            mst.prims_mst(source);

            decimal expectedCost = 7;

            // Act
            decimal actualCost = mst.print_distance(source);

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
            
        }

        [TestMethod]
        public void FindMinimumSpanningTreeTotalCost_ComplexTree_ReturnsCost()
        {
            // Arrange
            int nodes = 9;
            int edges = 14;
            PrimsMST mst = new PrimsMST();
            mst.initPrimsMST(nodes, edges);
            mst.AddNode(7,6,1);
            mst.AddNode(8,2,2);
            mst.AddNode(6,5,2);
            mst.AddNode(0,1,4);
            mst.AddNode(2,5,4);
            mst.AddNode(8,6,6);
            mst.AddNode(2,3,7);
            mst.AddNode(7,8,7);
            mst.AddNode(0,7,8);
            mst.AddNode(1,2,8);
            mst.AddNode(3,4,9);
            mst.AddNode(5,4,10);
            mst.AddNode(1,7,11);
            mst.AddNode(0,1,4);
            mst.AddNode(3,5,14);
            int source = 0;
            mst.prims_mst(source);


            decimal expectedCost = 37;

            // Act
            decimal actualCost = mst.print_distance(source);

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }

        [TestMethod]
        public void Test_Simple()
        {
            string sourceFile = "../../../MST1.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            PrimsMST pmst = new PrimsMST();
            
            decimal expectedCost = 5;

            // Act
            decimal actualCost = pmst.managePrimsMST(lines);

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }
        [TestMethod]
        public void TestIn2()
        {
            string sourceFile = "../../../MST2.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            PrimsMST pmst = new PrimsMST();
            
            decimal expectedCost = -3612829;

            // Act
            decimal actualCost = pmst.managePrimsMST(lines);

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }
        [TestMethod]
        public void TestIn3()
        {
            string sourceFile = "../../../MST3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            PrimsMST pmst = new PrimsMST();
            
            decimal expectedCost = 14;

            // Act
            decimal actualCost = pmst.managePrimsMST(lines);
        
            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }
        [TestMethod]
        public void TestIn4()
        {
            string sourceFile = "../../../MST4.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            PrimsMST pmst = new PrimsMST();

            decimal expectedCost = 4;

            // Act
            decimal actualCost = pmst.managePrimsMST(lines);;
        
            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }
    }
}