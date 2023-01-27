using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.BinarySearchTree;

namespace Test.DynamicProgramming
{
    
    [TestClass]
    public class OptimalBinarySearchTreeTest
    {
        [TestMethod]
        [DataRow(new string[] {"a", "aa", "aah"}, new int[] {8, 1, 1}, 13f)]
        [DataRow(new string[] {"a","aa","aah","aahed","aahing","aahs","aal","aalii","aaliis","aals","aardvark","aardvarks","aardwolf","aardwolves","aargh","aarrgh","aarrghh","aas","aasvogel","aasvogels","ab","aba","abaca","abacas","abaci","aback","abacterial","abacus","abacuses","abaft","abaka","abakas","abalone","abalones","abamp","abampere","abamperes","abamps","abandon","abandoned","abandoner","abandoners","abandoning","abandonment","abandonments","abandons","abapical","abas","abase","abased"}, new int[] {2,8,2,5,5,2,8,3,6,1,1,6,3,2,6,7,4,63,2,9,10,1,60,5,2,7,34,11,31,76,21,6,8,1,81,37,15,6,8,24,12,18,42,8,51,21,8,6,5,7}, 2780)]
        public void OptimalBinarySearchTree_Test(string[] keys, int[] freq, float result)
        {
            var algorithm = new Algorithm();
            Algorithm.KeyThreshold = 0;
            for(var i = 0;i<freq.Length;i++)
            {
                var word = new Word { Value = keys[i], Count = freq[i] };
                if(algorithm.Add(word))
                {
                    
                }
            }
            Debug.WriteLine($"Word Count: {freq.Length}");
            Debug.WriteLine($"Computing OptimalBst");
            var root = algorithm.OptimalBst();
            Debug.WriteLine($"Generating BST with optimal solution {root.Item1}");
            Assert.AreEqual(result, root.Item1);
            var bst = BST.FromTable(algorithm.Keys, root.Item2);

            Debug.WriteLine($"a - {bst.Search("a")}");
            Debug.WriteLine($"aa - {bst.Search("aa")}");
            Debug.WriteLine($"aah - {bst.Search("aah")}");
        }
    }
}
