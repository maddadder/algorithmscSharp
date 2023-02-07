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
        [DataRow(new string[] {"a", "aa", "aah", "aaah"}, new int[] {4, 2, 6, 3}, 26)]
        [DataRow(new string[] {"a", "aa", "aah"}, new int[] {8, 1, 1}, 13)]
        [DataRow(new string[] {"a","aa","aah","aahed","aahing","aahs","aal","aalii","aaliis","aals","aardvark","aardvarks","aardwolf","aardwolves","aargh","aarrgh","aarrghh","aas","aasvogel","aasvogels","ab","aba","abaca","abacas","abaci","aback","abacterial","abacus","abacuses","abaft","abaka","abakas","abalone","abalones","abamp","abampere","abamperes","abamps","abandon","abandoned","abandoner","abandoners","abandoning","abandonment","abandonments","abandons","abapical","abas","abase","abased"}, new int[] {2,8,2,5,5,2,8,3,6,1,1,6,3,2,6,7,4,63,2,9,10,1,60,5,2,7,34,11,31,76,21,6,8,1,81,37,15,6,8,24,12,18,42,8,51,21,8,6,5,7}, 2780)]
        public void OptimalBinarySearchTree_Test(string[] words, int[] weights, int result)
        {
            var algorithm = new Algorithm();
            Algorithm.WeightThreshold = 0;
            for(var i = 0;i<weights.Length;i++)
            {
                var data = new NodeData { Value = words[i], Count = weights[i] };
                if(algorithm.Add(data))
                {
                    
                }
            }
            Debug.WriteLine($"Row Count: {weights.Length}");
            Debug.WriteLine($"Computing OptimalBst");
            var root = algorithm.OptimalBst();
            Debug.WriteLine($"Generating BST with optimal solution: {root.Item1}");
            
            var bst = Node.FromTable(algorithm.Keys, root.Item2);

            Debug.WriteLine($"a - {bst.Search("a")}");
            Debug.WriteLine($"aa - {bst.Search("aa")}");
            Debug.WriteLine($"aah - {bst.Search("aah")}");
            Assert.AreEqual(result, root.Item1);
        }

        [TestMethod]
        [DataRow(new string[] {"a", "aa", "aah"}, new int[] {34, 8, 50}, 142)]
        [DataRow(new string[] {"a","aa","aah","aahed","aahing","aahs","aal","aalii","aaliis","aals","aardvark","aardvarks","aardwolf","aardwolves","aargh","aarrgh","aarrghh","aas","aasvogel","aasvogels","ab","aba","abaca","abacas","abaci","aback","abacterial","abacus","abacuses","abaft","abaka","abakas","abalone","abalones","abamp","abampere","abamperes","abamps","abandon","abandoned","abandoner","abandoners","abandoning","abandonment","abandonments","abandons","abapical","abas","abase","abased"}, new int[] {2,8,2,5,5,2,8,3,6,1,1,6,3,2,6,7,4,63,2,9,10,1,60,5,2,7,34,11,31,76,21,6,8,1,81,37,15,6,8,24,12,18,42,8,51,21,8,6,5,7}, 2780)]
        public void OptimalBinarySearchTree_RecursiveTest(string[] words, int[] weights, int result)
        {
            int n = weights.Length;
            var algorithm = new Algorithm();
            Algorithm.WeightThreshold = 0;
            for(var i = 0;i<weights.Length;i++)
            {
                var data = new NodeData { Value = words[i], Count = weights[i] };
                if(algorithm.Add(data))
                {
                    
                }
            }
            var keys = algorithm.Keys.Select(x => x.Count).ToArray();
            var cost = algorithm.OptimalBstRecSlow(keys);
            Debug.WriteLine($"Cost of Optimal BST is {cost}");
            
            var bst = Node.FromTable(algorithm.Keys, algorithm.root);

            Debug.WriteLine($"a - {bst.Search("a")}");
            Debug.WriteLine($"aa - {bst.Search("aa")}");
            Debug.WriteLine($"aah - {bst.Search("aah")}");
            Assert.AreEqual(result, cost);
        }

        [TestMethod]
        public void Test_Tiny_OBST()
        {
            string sourceFile = "../../../../Data/tinydictionary.txt";
            AnimatedAlgorithm.WeightThreshold = 0;
            var lines = File.ReadLines(sourceFile);
            var algorithm = new AnimatedAlgorithm();

            int i = 0;
            foreach (var line in lines)
            {
                var data = NodeData.FromLine(line);
                if(algorithm.Add(data))
                {
                    i++;
                }
            }
            var root = algorithm.OptimalBst();
        }

        [TestMethod]
        public void Test_OBST()
        {
            Console.WriteLine($"Start Time: {DateTime.Now}");
            //https://github.com/jackchammons/wordFrequency
            string sourceFile = "../../../../Data/dictionary.txt";
            //string sourceFile = "../../../../Data/smalldictionary.txt";
            //Algorithm.WeightThreshold = 0;
            var lines = File.ReadLines(sourceFile);

            //https://github.com/gabrielKerekes/OptimalBSTAlgorithm
            var algorithm = new Algorithm();

            int i = 0;
            foreach (var line in lines)
            {
                var data = NodeData.FromLine(line);
                if(algorithm.Add(data))
                {
                    i++;
                }
            }
            Console.WriteLine($"NodeData Count: {i}");
            Console.WriteLine($"Computing OptimalBst");
            var root = algorithm.OptimalBst();
            Console.WriteLine($"Generating BST with optimal solution {root.Item1}");
            var bst = Node.FromTable(algorithm.Keys, root.Item2);
            Console.WriteLine($"End Time: {DateTime.Now}");
            //Node.Display(bst, 4);
            while(true){
                var text = Console.ReadLine();
                Console.WriteLine($"{text} - {bst.Search(text)}");
            }
        }
        [TestMethod]
        public void Test_OBSTRecSlow()
        {
            Console.WriteLine($"Start Time: {DateTime.Now}");
            //https://github.com/jackchammons/wordFrequency
            string sourceFile = "../../../../Data/tinydictionary.txt";
            //string sourceFile = "../../../../Data/smalldictionary.txt";
            //Algorithm.WeightThreshold = 0;
            var lines = File.ReadLines(sourceFile);

            //https://github.com/gabrielKerekes/OptimalBSTAlgorithm
            var algorithm = new Algorithm();

            int i = 0;
            foreach (var line in lines)
            {
                var data = NodeData.FromLine(line);
                if(algorithm.Add(data))
                {
                    i++;
                }
            }
            Console.WriteLine($"NodeData Count: {i}");
            Console.WriteLine($"Computing OptimalBst");
            var keys = algorithm.Keys.Select(x => x.Count).ToArray();
            var cost = algorithm.OptimalBstRecSlow(keys);
            Console.WriteLine($"Cost of Optimal BST is {cost}");

            var bst = Node.FromTable(algorithm.Keys, algorithm.root);
            Console.WriteLine($"End Time: {DateTime.Now}");
            //Node.Display(bst, 4);
            while(true){
                var text = Console.ReadLine();
                Console.WriteLine($"{text} - {bst.Search(text)}");
            }
        }
    }
}
