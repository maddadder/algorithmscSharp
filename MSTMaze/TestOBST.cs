using Lib.Graphs;
using Lib.BinarySearchTree;

namespace OBST 
{
    public class TestOBST
    {
        public TestOBST(){
            
        }
        public static void Test_Tiny_OBST()
        {
            string sourceFile = "../../../tinydictionary.txt";
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
        public static void Test_OBST()
        {
            Console.WriteLine($"Start Time: {DateTime.Now}");
            //https://github.com/jackchammons/wordFrequency
            string sourceFile = "../../../dictionary.txt";
            //string sourceFile = "../../../smalldictionary.txt";
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
        //Visualize online at https://graphonline.ru/en/
        public static void Test_OBSTRecSlow()
        {
            Console.WriteLine($"Start Time: {DateTime.Now}");
            //https://github.com/jackchammons/wordFrequency
            string sourceFile = "../../../dictionary.txt";
            //string sourceFile = "../../../smalldictionary.txt";
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