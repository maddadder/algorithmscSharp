using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BinarySearchTree
{
    public class Algorithm
    {
        public Algorithm(int sumOfCounts){
            SumOfCounts = sumOfCounts;
        }
        
        public static int SumOfCounts;
        public const int KeyThreshold = 50;


        public List<Word> Keys { get; set; } = new List<Word>();

        public void Add(Word word)
        {
            if (word.Count > KeyThreshold)
                AddKey(word);
        }

        private void AddKey(Word key)
        {
            Keys.Add(key);
        }
        
        public Tuple<double[][], int[][]> OptimalBst()
        {
            var keyProbs = Keys.Select(k => k.Probability).ToArray();
            return OptimalBst(keyProbs);
        }

        public static Tuple<double[][], int[][]> OptimalBst(double[] keyProbs)
        {
            var keyProbsList = keyProbs.ToList();
            keyProbsList.Insert(0, 0);
            keyProbs = keyProbsList.ToArray();

            var n = keyProbs.Length - 1;

            var e = new double[n + 2][];
            var w = new double[n + 2][];
            var root = new int[n][];
            for (var i = 1; i <= n + 1; i++)
            {
                e[i] = new double[n + 1];
                w[i] = new double[n + 1];
                if (i <= n)
                    root[i - 1] = new int[n];
            }

            for (var l = 1; l <= n; l++)
            {
                for (var i = 1; i <= n - l + 1; i++)
                {
                    var j = i + l - 1;
                    e[i][j] = double.PositiveInfinity;
                    w[i][j] = w[i][j - 1] + keyProbs[j];

                    for (var r = i; r <= j; r++)
                    {
                        var t = e[i][r - 1] + e[r + 1][j] + w[i][j];
                        if (t < e[i][j])
                        {
                            e[i][j] = t;
                            root[i - 1][j - 1] = r;
                        }
                    }
                }
            }

            return Tuple.Create(e, root);
        }
    }
}
 