using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Model;
using Extensions;
using Lib.DynamicProgramming;
using BinarySearchTree;

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
            string sourceFile = "../../../dictionary.txt";
            var lines = File.ReadLines(sourceFile);
            
            //https://github.com/gabrielKerekes/OptimalBSTAlgorithm
            var algorithm = new Algorithm(lines.Count());


            int i = 0;
            foreach (var line in lines)
            {
                var word = Word.FromLine(line);
                algorithm.Add(word);
                i++;
                if(i > 5000)
                    break;
            }

            var result = algorithm.OptimalBst();

            var bst = BST.FromTable(algorithm.Keys, result.Item2);
            Debug.WriteLine(BSTCost.Calculate(bst));
            Debug.WriteLine($"a - {bst.Search("a")}");
            Debug.WriteLine($"aardvark - {bst.Search("aardvark")}");
            Debug.WriteLine($"aals - {bst.Search("aals")}");
            Debug.WriteLine($"aluminum - {bst.Search("aluminum")}");
            Debug.WriteLine($"mr - {bst.Search("mr")}");
            Debug.WriteLine($"very - {bst.Search("very")}");
            Debug.WriteLine($"year - {bst.Search("year")}");
            Debug.WriteLine($"yes - {bst.Search("yes")}");
            Debug.WriteLine($"our - {bst.Search("our")}");
            Debug.WriteLine($"might - {bst.Search("might")}");
        }
    }
}
