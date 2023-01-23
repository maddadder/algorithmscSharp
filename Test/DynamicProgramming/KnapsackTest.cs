using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Model;
using Extensions;

namespace Test.DynamicProgramming
{
    
    [TestClass]
    public class KnapsackTest
    {
        [TestMethod]
        public void Knapsack_Test(){
            var jobs = new List<Item>();
            Random rand = new Random();
            var Budget = 10;
            int cost = Budget*2;
            for(var i = 0;i<Budget;i++){
                var val = rand.Next(1,Budget);
                jobs.Add(new Item(val,val));
            }
            var result = knapsack(jobs.ToArray(), cost);
            Debug.WriteLine($"The total value (in weight) of the items in the knapsack is {result}.");
            Debug.WriteLine($"The bag can only carry {cost}");
        }
        [TestMethod]
        //[DataRow(new int[] { 3,2,4,4 }, new int[] { 4,3,2,3 })]
        [DataRow(new int[] { 5,4,3,2 }, new int[] { 4,3,2,1 })]
        public void Knapsack_TestWithFourItems(int[] values, int[] costs){
            var cost = 6;
            Item[] jobs = new Item[values.Length];
            for(var i = 0;i<values.Length;i++){
                jobs[i] = new Item(values[i], costs[i]);
            }
            var value = knapsack(jobs.ToArray(), cost);
            Debug.WriteLine($"The total value (in weight) of the items in the knapsack is {value}.");
            Debug.WriteLine($"The bag can only carry {cost}");
        }
        public int knapsack(Item[] S, int Capacity)
        {
            // subproblem solutions
            int[, ] matrix = new int[S.Length+1, Capacity+1];

            // base case
            for(var c = 0;c<=Capacity;c++)
            {
                matrix[0, c] = 0;
            }

            // systematically solve all subproblems
            for(var i = 1;i<=S.Length;i++) {
                for(var c = 1;c<=Capacity;c++) {
                    // use recurrence from Corollary 16.5
                    var case1 = matrix[i-1, c];
                    var Si = S[i-1].Cost();
                    var Vi = S[i-1].Value();
                    if (Si > c) {
                        // where case1 is an optimal solution for the first i-1 items with 
                        // knapsack capacity = Capacity
                        matrix[i, c] = case1; //peach
                        Debug.Write("peach,");
                    } else {
                        // where case2 is an optimal solution for the first i-1 items with
                        // knapsack capacity = Capacity - Si, supplemented with the last item Vi
                        var case2 = matrix[i-1, c-Si] + Vi;
                        if(case1.CompareTo(case2) > 0) {
                            matrix[i,c] = case1; //yellow
                            Debug.Write("yellow,");
                        } else {
                            matrix[i,c] = case2; //blue
                            Debug.Write("blue,");
                        }
                    }
                }
                Debug.WriteLine("");
            }
            return matrix[S.Length, Capacity]; //solution to largest subproblem
        }
    }
}
