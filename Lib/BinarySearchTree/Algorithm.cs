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

        private int[,] cache;
        public int[,] root;
        // A recursive function to calculate cost of
        // optimal binary search tree
        int OptimalBstRecSlow(int[] freq, int i, int j)
        {
            
            // Base cases
            // no elements in this subarray
            if (i == j + 1)    
                return 0;
            
            // one element in this subarray   
            if (j == i)    
                return cache[i,j];
            
            // Reuse cost already calculated for the subproblems.
            if (cache[i,j] > 0){
                return cache[i,j];
            }

            // Get sum of freq[i], freq[i+1], ... freq[j]
            int fsum = sum(freq, i, j);
            
            // Initialize minimum value
            int min = int.MaxValue;
            
            // One by one consider all elements as root and
            // recursively find cost of the BST, compare the
            // cost with min and update min if needed
            for (int r = i; r <= j; r++)
            {
                int cost = OptimalBstRecSlow(freq, i, r-1) +
                                OptimalBstRecSlow(freq, r+1, j) + fsum;
                if (cost < min){
                    min = cost;
                    cache[i,j] = cost;
                    root[i-1,j-1] = r;
                }
            }
            
            // Return minimum value
            return min;
        }
        
        // The main function that calculates minimum cost of
        // a Binary Search Tree. It mainly uses OptimalBstRecSlow() to
        // find the optimal cost.
        public int OptimalBstRecSlow(int[] freq)
        {
            var n = freq.Length;
            root = new int[n,n];
            cache = new int[n+1,n+1];
            for (var i=0;i<n;i++){
                cache[i+1,i+1] = freq[i];
                root[i,i] = i + 1;
            }
            return OptimalBstRecSlow(freq, 1, n);
        }
        
        // A utility function to get sum of array elements
        // freq[i] to freq[j]
        int sum(int[] freq, int i, int j)
        {
            int s = 0;
            for (int k = i; k <= j; k++)
            s += freq[k-1];
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
 