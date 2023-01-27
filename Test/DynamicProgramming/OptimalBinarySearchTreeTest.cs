using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Model;
using Extensions;
using Lib.DynamicProgramming;
using Lib.BinarySearchTree;

namespace Test.DynamicProgramming
{
    
    [TestClass]
    public class OptimalBinarySearchTreeTest
    {
        
        [TestMethod]
        [DataRow(new int [] {1, 2, 3}, new float[] {.8f, .1f, .1f})]
        public void OptimalBinarySearchTree_Test(int[] keys, float[] freq)
        {
            OptimalBinarySearchTree obst = new OptimalBinarySearchTree();
            var n = keys.Length;
            var value = obst.ComputeCost(keys, freq, n);
            Debug.WriteLine($"Result: {value}.");
            Assert.AreEqual(1.3f, value);
        }
        [TestMethod]
        public void OptimalBinarySearchTree_Test2()
        {
            //https://github.com/jackchammons/wordFrequency
            string sourceFile = "../../../smalldictionary.txt";
            var lines = File.ReadLines(sourceFile);
            
            //https://github.com/gabrielKerekes/OptimalBSTAlgorithm
            var algorithm = new Algorithm();
            Algorithm.KeyThreshold = 0;
            int i = 0;
            foreach (var line in lines)
            {
                var word = Word.FromLine(line);
                if(algorithm.Add(word))
                {
                    i++;
                }
            }
            Debug.WriteLine($"Word Count: {i}");
            Algorithm.SumOfCounts = i;
            var root = algorithm.OptimalBst();

            var bst = BST.FromTable(algorithm.Keys, root);
            Debug.WriteLine(BSTCost.Calculate(bst));
            Debug.WriteLine($"alice - {bst.Search("alice")}");
            Debug.WriteLine($"bob - {bst.Search("bob")}");
            Debug.WriteLine($"charlie - {bst.Search("charlie")}");
            Debug.WriteLine($"disruptive - {bst.Search("disruptive")}");
            Debug.WriteLine($"equipment - {bst.Search("equipment")}");
            Debug.WriteLine($"for - {bst.Search("for")}");
            Debug.WriteLine($"go - {bst.Search("go")}");
            Debug.WriteLine($"helped - {bst.Search("helped")}");
            Debug.WriteLine($"i - {bst.Search("i")}");
            Debug.WriteLine($"journal - {bst.Search("journal")}");
        }
    }
}
