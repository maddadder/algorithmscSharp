using System;
using Lib.BinarySearchTree;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Lib.DynamicProgramming;
namespace Lib.Wordle
{
    public class WordleSolver
    {
        public void Wordle()
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

            Node.insertChildrenCount(bst);
 
            Console.WriteLine( "A Random Node From Tree : " +
                                    Node.randomNode(bst));
            SequenceAligner sq = new SequenceAligner();
            //Node.Display(bst, 4);
            
            while(true){
                var word = "panther";
                var matches = new List<Tuple<string,int>>();
                
                var contains = "";
                var badchars = "";
                var tries = 0;
                var answers = new Dictionary<string, int>();
                Console.WriteLine("word to match? (Required)");
                word = Console.ReadLine();
                var depth = bst.Search(word);
                Console.WriteLine(depth);
                while(depth == int.MinValue || depth == int.MaxValue)
                {
                    Console.WriteLine("word to match? (Required)");
                    word = Console.ReadLine();
                    depth = bst.Search(word);
                    Console.WriteLine(depth);
                }
                Console.WriteLine();
                var indicies = "blank";
                while(indicies.Length > 0){
                    Console.WriteLine("character to match?");
                    indicies = Console.ReadLine();
                    if(!string.IsNullOrEmpty(indicies))
                    {
                        var position = "";
                        Console.WriteLine($"position to match {indicies}");
                        position = Console.ReadLine();
                        var positionInt = -1;
                        if(!int.TryParse(position, out positionInt)){
                            indicies = string.Empty; //break while loop
                        }
                        else if(positionInt < 0)
                        {
                            indicies = string.Empty; //break while loop
                        }
                        else{
                            matches.Add(new Tuple<string,int>(indicies, positionInt));
                        }
                    }
                }
                Console.WriteLine("contains characters?");
                contains = Console.ReadLine();
                Console.WriteLine("does not contains characters?");
                badchars = Console.ReadLine();
                while(contains.ToCharArray().Intersect(badchars.ToCharArray()).Any()){
                    Console.WriteLine("contains characters?");
                    contains = Console.ReadLine();
                    Console.WriteLine("does not contains character?");
                    badchars = Console.ReadLine();
                }
                var maxloops = algorithm.Keys.Count * 1000;
                var loops = 0;
                while(tries < 500){
                    var attempt = Node.randomNode(bst);
                    //var cost = bst.Search(attempt);
                    if(attempt.Length == word.Length)
                    {
                        //Console.WriteLine($"{attempt}");
                        var score = sq.sequenceAlignment(word.ToCharArray(), attempt.ToCharArray(), 4, 5);
                        var aligned = sq.sequenceAlignmentReconstruction(word.ToCharArray(), attempt.ToCharArray(), 4, 5);
                        foreach(var entry in matches)
                        {
                            if(aligned.Value[entry.Item2] == entry.Item1.ToCharArray()[0]){
                                var badIntersect = aligned.Value.ToCharArray().Intersect(badchars.ToCharArray());
                                if(!badIntersect.Any())
                                {
                                    var containsIntersect = aligned.Value.ToCharArray().Intersect(contains.ToCharArray());
                                    if(containsIntersect.Count() == contains.ToCharArray().Length){
                                        if(!answers.ContainsKey(aligned.Value))
                                        {
                                            answers.Add(aligned.Value,score);
                                        }
                                        tries+=1;
                                    }
                                }
                            }
                        }
                        if(matches.Count == 0){
                            var badIntersect = aligned.Value.ToCharArray().Intersect(badchars.ToCharArray());
                            if(!badIntersect.Any())
                            {
                                var containsIntersect = aligned.Value.ToCharArray().Intersect(contains.ToCharArray());
                                if(containsIntersect.Count() == contains.ToCharArray().Length){
                                    if(!answers.ContainsKey(aligned.Value))
                                    {
                                        answers.Add(aligned.Value,score);
                                    }
                                    tries+=1;
                                }
                            }
                        }
                    }
                    loops+=1;
                    if(loops > maxloops){
                        break;
                    }
                }
                foreach(var item in answers.OrderBy(x => x.Value)){
                    Console.WriteLine($"{item.Key},{item.Value}");
                }
            }
        }
        
    }
}
