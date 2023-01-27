using Lib.Graphs;
using Lib.BinarySearchTree;

namespace OBST 
{
    public class TestOBST
    {
        public TestOBST(){
            
        }

        //Visualize online at https://graphonline.ru/en/
        public static void Test_OBST()
        {
            //https://github.com/jackchammons/wordFrequency
            string sourceFile = "../../../dictionary.txt";
            //string sourceFile = "../../../smalldictionary.txt";
            //Algorithm.KeyThreshold = 0;
            var lines = File.ReadLines(sourceFile);

            //https://github.com/gabrielKerekes/OptimalBSTAlgorithm
            var algorithm = new Algorithm();

            int i = 0;
            foreach (var line in lines)
            {
                var word = Word.FromLine(line);
                if(algorithm.Add(word))
                {
                    i++;
                }
            }
            Console.WriteLine($"Word Count: {i}");
            Algorithm.SumOfCounts = i;
            var root = algorithm.OptimalBst();

            var bst = BST.FromTable(algorithm.Keys, root);
            Console.WriteLine(BSTCost.Calculate(bst));
            while(true){
                var text = Console.ReadLine();
                Console.WriteLine($"{text} - {bst.Search(text)}");
            }
        }
    }
}