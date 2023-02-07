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
            string sourceFile = "../../../../Data/dictionary.txt";
            var lines = File.ReadLines(sourceFile);
            SequenceAligner sq = new SequenceAligner();
            
            while(true){
                var word = "panther";
                var matches = new List<Tuple<string,int>>();
                
                var contains = "";
                var badchars = "";
                var tries = 0;
                var answers = new Dictionary<string, int>();
                Console.WriteLine("word to match? (Required)");
                word = Console.ReadLine();
                var found = lines.Select(x => NodeData.FromLine(x).Value).Contains(word);
                while(found == false)
                {
                    Console.WriteLine("word to match? (Required)");
                    word = Console.ReadLine();
                    found = lines.Select(x => NodeData.FromLine(x).Value).Contains(word);
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
                foreach (var line in lines)
                {
                    var data = NodeData.FromLine(line);
                    var attempt = data.Value;
                    if(attempt.Length == word.Length)
                    {
                        var score = sq.sequenceAlignment(word.ToCharArray(), attempt.ToCharArray(), 4, 5);
                        var aligned = sq.sequenceAlignmentReconstruction(word.ToCharArray(), attempt.ToCharArray(), 4, 5);
                        var badIntersect = aligned.Value.ToCharArray().Intersect(badchars.ToCharArray());
                        if(!badIntersect.Any())
                        {
                            var containsIntersect = aligned.Value.ToCharArray().Intersect(contains.ToCharArray());
                            if(containsIntersect.Count() == contains.ToCharArray().Length){
                                foreach(var index in matches)
                                {
                                    if(aligned.Value[index.Item2] == index.Item1.ToCharArray()[0])
                                    {
                                        if(!answers.ContainsKey(aligned.Value))
                                        {
                                            answers.Add(aligned.Value,score);
                                        }
                                        tries+=1;
                                    }
                                }
                                if(matches.Count == 0)
                                {
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
                foreach(var item in answers.OrderBy(x => x.Value)){
                    Console.WriteLine($"{item.Key},{item.Value}");
                }
            }
        }
        
    }
}
