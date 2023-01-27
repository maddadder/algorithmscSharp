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
        
        public static int KeyThreshold = 12500;


        public List<NodeData> Keys { get; set; } = new List<NodeData>();

        public bool Add(NodeData data)
        {
            if (data.Count > KeyThreshold){
                AddKey(data);
                return true;
            }
            return false;
        }

        private void AddKey(NodeData key)
        {
            Keys.Add(key);
        }
        
        public Tuple<int, int[,]> OptimalBst()
        {
            var keyProbs = Keys.Select(k => k.Count).ToArray();
            return OptimalBst(keyProbs);
        }

        public static Tuple<int, int[,]> OptimalBst(int[] keyProbs)
        {
            var n = keyProbs.Length;
            var e = new int[n+1,n+1];
            var root = new int[n,n];

            for (var s = 0; s <= n; s++)
            {
                for (var i = 1;i<=n-s; i++)
                {
                    var j = i + s;
                    var pk = sum(keyProbs, i-1, j);
                    var min_cost = int.MaxValue;
                    // we brute force compute the minimum cost of each root, excluding any recomputations.
                    for (var r = i; r<=j;r++){
                        if(min_cost.CompareTo(e[i-1,r-1] + e[r,j]) > 0){
                            min_cost = e[i-1,r-1] + e[r,j];
                            root[i - 1,j - 1] = r;
                        }
                    }
                    // we store the sum of the cost of the tree
                    e[i-1,j] = pk + min_cost;
                    //print(e);
                }
            }

            return new Tuple<int, int[,]>(e[0,n],root);
        }

        // A utility function to get sum of array elements
        // freq[i] to freq[j]
        static int sum(int[] freq, int i, int j)
        {
            int s = 0;
            for (int k = i; k < j; k++)
                s += freq[k];
            return s;
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
 