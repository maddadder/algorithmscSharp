using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lib.BinarySearchTree
{
    public class Algorithm
    {
        public static int WeightThreshold = 12500;
        public List<NodeData> Keys { get; set; } = new List<NodeData>();

        public bool Add(NodeData data)
        {
            if (data.Count > WeightThreshold){
                Keys.Add(data);
                return true;
            }
            return false;
        }
        
        public Tuple<int, int[,]> OptimalBst()
        {
            var weights = Keys.Select(k => k.Count).ToArray();
            return OptimalBst(weights);
        }

        public static Tuple<int, int[,]> OptimalBst(int[] weights)
        {
            var n = weights.Length;
            var e = new int[n+1,n+1];
            var w = new int[n+1,n+1];
            var root = new int[n,n];

            for (var s = 0; s <= n; s++)
            {
                for (var i = 1;i<=n-s; i++)
                {
                    var j = i + s;
                    w[i-1,j] = w[i-1,j-1] + weights[j-1];
                    var min_cost = int.MaxValue;
                    // we brute force compute the minimum cost of each root, excluding any recomputations.
                    for (var r = i; r<=j;r++){
                        if(min_cost.CompareTo(e[i-1,r-1] + e[r,j]) > 0){
                            min_cost = e[i-1,r-1] + e[r,j];
                            root[i - 1,j - 1] = r;
                        }
                    }
                    // we store the sum of the cost of the tree
                    e[i-1,j] = w[i-1,j] + min_cost;
                    //print(e);
                }
            }

            return new Tuple<int, int[,]>(e[0,n],root);
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
 