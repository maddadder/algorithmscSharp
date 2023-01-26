using System;

namespace Lib.DynamicProgramming
{
    public class OptimalBinarySearchTree
    {
        float[, ] matrix;

        // A utility function to get sum of array elements
        // freq[i] to freq[j]
        float sum(float[] freq, int i, int j)
        {
            float s = 0;
            for (int k = i; k < j; k++)
                s += freq[k];
            return s;
        }
        public float ComputeCost(int[] keys, float[] freq, int n)
        {
            // subproblem solutions
            matrix = new float[n+1, n+1];

            // base cases (i = j + 1)
            for(var i = 0;i<=n;i++)
            {
                matrix[i, i] = 0;
            }
            print(matrix);
            // systematically solve all subproblems (i <= j)
            for (var s = 0;s<=n-1;s++){ // s=subproblem size-1
                for (var i = 1;i<=n-s;i++){ 
                    var j = i+s;
                    var pk = sum(freq, i-1, j);
                    var min_cost = float.MaxValue;
                    // we brute force compute the minimum cost of each root, excluding any recomputations.
                    for (var r = i; r<=j;r++){
                        min_cost = Math.Min(min_cost, matrix[i-1,r-1] + matrix[r,j]);
                    }
                    // we store the sum of the cost of the tree
                    matrix[i-1,j] = pk + min_cost;
                    print(matrix);
                }
            }
            
            return matrix[0,n];
        }
        private void print(float[, ] matrix)
        {
            Console.SetCursorPosition(0,1);
            for(var i=0;i<matrix.GetLength(0);i++){
                for(var j=0;j<matrix.GetLength(1);j++){
                    if(matrix[i,j] > 0)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(matrix[i,j].ToString("0.0")+",");
                    System.Threading.Thread.Sleep(100);
                }
                Console.WriteLine("");
            }
        }
    }
}
