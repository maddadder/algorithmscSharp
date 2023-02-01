using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lib.BinarySearchTree
{
    public class AnimatedAlgorithm
    {
        public static int WeightThreshold = 304;
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
                    var weight = w[i-1,j];
                    int weight_i = i-1;
                    int weight_j = j;
                    var min_cost = int.MaxValue;

                    var l_index = i;
                    var r_index = j;
                    // kuths speedup begin from O(n^3) to O(n^2)
                    /*
                    if(i!=j)
                    {
                        l_index = root[i-1,j-2];
                        r_index = root[i,j-1];
                    }*/
                    // kuths speedup end

                    int min_left_r = 0;
                    int min_left_j = 0;
                    int min_right_r = 0;
                    int min_right_j = 0;
                    for (var r = l_index; r<=r_index;r++){
                        var cost = e[i-1,r-1] + e[r,j];
                        if(min_cost.CompareTo(e[i-1,r-1] + e[r,j]) > 0){
                            min_cost = e[i-1,r-1] + e[r,j];
                            root[i - 1,j - 1] = r;
                            min_left_r = i-1;
                            min_left_j = r-1;
                            min_right_r = r;
                            min_right_j = j;
                        }
                        int left_r = i-1;
                        int left_j = r-1;
                        int right_r = r;
                        int right_j = j;
                        print(e, w,weight, cost, min_cost, left_r, left_j, right_r, right_j, weight_i, weight_j);
                    }
                    // we store the sum of the cost of the tree
                    e[i-1,j] = w[i-1,j] + min_cost;
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
        private static void print(int[, ] matrix, int[, ] w, int weight, int cost, int min_cost, int left_r, int left_j, int right_r, int right_j, int weight_i, int weight_j)
        {
            Console.SetCursorPosition(0,1);
            for(var i=0;i<matrix.GetLength(0);i++){
                for(var j=0;j<matrix.GetLength(1);j++){
                    if(left_r == i && left_j == j){
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if(right_r == i && right_j == j){
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if(matrix[i,j] > 0)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(matrix[i,j].ToString("00")+",");
                }
                Console.WriteLine("");
            }
            Console.SetCursorPosition(0,w.GetLength(1)+2);
            for(var i=0;i<w.GetLength(0);i++){
                for(var j=0;j<w.GetLength(1);j++){
                    if(weight_i == i && weight_j == j){
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if(w[i,j] > 0)
                        Console.ForegroundColor = ConsoleColor.White;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(w[i,j].ToString("00")+",");
                }
                Console.WriteLine("");
            }
            Console.SetCursorPosition(0,matrix.GetLength(1)+w.GetLength(1)+3);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"weight: w[{weight_i},{weight_j}]={weight.ToString("00")}        \n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"cost: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"e[{left_r},{left_j}]={matrix[left_r,left_j]}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"+e[{right_r},{right_j}]={matrix[right_r,right_j]}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"=={cost.ToString("00")}          \n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"min_cost?: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"weight");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"+");
            Console.Write($"cost");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"=");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{(weight+cost).ToString("00")}       \n");
            Console.Write($"min_cost: {(weight+min_cost).ToString("00")}       \n");
            var second = 10;
            var sleepCount = 2;
            if(cost > 0)
                sleepCount = 10;
            Console.ForegroundColor = ConsoleColor.White;
            while(sleepCount > 0){
                Console.SetCursorPosition(0,matrix.GetLength(1)+w.GetLength(1)+7);
                sleepCount-=1;
                Console.WriteLine($"Countdown: {sleepCount}");
                System.Threading.Thread.Sleep(second);
            }
        }
    }
}
 