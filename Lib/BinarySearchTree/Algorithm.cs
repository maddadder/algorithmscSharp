using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lib.BinarySearchTree
{
    public class Algorithm
    {
        public Algorithm(){
            
        }
        
        public static int SumOfCounts;
        public static int KeyThreshold = 12500;


        public List<Word> Keys { get; set; } = new List<Word>();

        public bool Add(Word word)
        {
            if (word.Count > KeyThreshold){
                AddKey(word);
                return true;
            }
            return false;
        }

        private void AddKey(Word key)
        {
            Keys.Add(key);
        }
        
        public int[,] OptimalBst()
        {
            var keyProbs = Keys.Select(k => k.Probability).ToArray();
            return OptimalBst(keyProbs);
        }

        public static int[,] OptimalBst(double[] keyProbs)
        {
            var keyProbsList = keyProbs.ToList();
            keyProbsList.Insert(0, 0);
            keyProbs = keyProbsList.ToArray();

            var n = keyProbs.Length - 1;

            var e = new double[n+1,n+1];
            var w = new double[n+1,n+1];
            var root = new int[n,n];

            for (var s = 0; s <= n; s++)
            {
                for (var i = 1;i<=n-s; i++)
                {
                    var j = i + s;
                    e[i-1,j] = double.PositiveInfinity;
                    w[i-1,j] = w[i-1,j - 1] + keyProbs[j];

                    for (var r = i; r <= j; r++)
                    {
                        var t = e[i-1,r-1] + e[r,j] + w[i-1,j];
                        if (t < e[i-1,j])
                        {
                            e[i-1,j] = t;
                            root[i - 1,j - 1] = r;
                            //print(root);
                        }
                    }
                }
            }

            return root;
        }
        private static void print(int[, ] matrix)
        {
            Console.SetCursorPosition(0,1);
            for(var i=0;i<matrix.GetLength(0);i++){
                for(var j=0;j<matrix.GetLength(1);j++){
                    if(matrix[i,j] > 0)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(matrix[i,j].ToString("0")+",");
                    System.Threading.Thread.Sleep(50);
                }
                Console.WriteLine("");
            }
        }
    }
}
 