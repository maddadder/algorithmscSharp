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
            var word = "panther";
            var matches = new Dictionary<string,int>();
            matches.Add("a",1);
            matches.Add("e",5);
            matches.Add("r",6);
            var contains = "aer".ToCharArray();
            var badchars = "pnthdvisgcwy".ToCharArray();
            var tries = 0;
            var answers = new Dictionary<string, int>();
            while(tries < 500){
                var attempt = Node.randomNode(bst);
                //var cost = bst.Search(attempt);
                if(attempt.Length == word.Length)
                {
                    //Console.WriteLine($"{attempt}");
                    var score = sq.sequenceAlignment(word.ToCharArray(), attempt.ToCharArray(), 4, 5);
                    var aligned = sq.sequenceAlignmentReconstruction(word.ToCharArray(), attempt.ToCharArray(), 4, 5);
                    if(aligned.Value != word){
                        foreach(var entry in matches)
                        {
                            if(aligned.Value[entry.Value] == entry.Key.ToCharArray()[0]){
                                var badIntersect = aligned.Value.ToCharArray().Intersect(badchars);
                                if(!badIntersect.Any())
                                {
                                    var containsIntersect = aligned.Value.ToCharArray().Intersect(contains);
                                    if(containsIntersect.Count() == contains.Length){
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
                            var badIntersect = aligned.Value.ToCharArray().Intersect(badchars);
                            if(!badIntersect.Any())
                            {
                                var containsIntersect = aligned.Value.ToCharArray().Intersect(contains);
                                if(containsIntersect.Count() == contains.Length){
                                    if(!answers.ContainsKey(aligned.Value))
                                    {
                                        answers.Add(aligned.Value,score);
                                    }
                                    tries+=1;
                                }
                            }
                        }
                    }
                }
            }
            foreach(var item in answers.OrderBy(x => x.Value)){
                Console.WriteLine($"{item.Key},{item.Value}");
            }
        }
        
    }
}
