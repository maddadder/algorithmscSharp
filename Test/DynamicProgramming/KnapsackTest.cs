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
            var jobs = new List<Job>();
            Random rand = new Random();
            var Budget = 10000;
            int cost = Budget*2;
            for(var i = 0;i<Budget;i++){
                var val = rand.Next(1,Budget);
                jobs.Add(new Job(val,val));
            }
            var result = knapsack(jobs.ToArray(), cost);
            Debug.WriteLine($"The total value (in length) of the items in the knapsack is {result}.");
            Debug.WriteLine($"The bag can only carry {cost}");
        }
        [TestMethod]
        [DataRow(new int[] { 3,2,4,4 }, new int[] { 4,3,2,3 })]
        [DataRow(new int[] { 3,2,4,4 }, new int[] { 3,2,4,4 })]
        [DataRow(new int[] { 4,3,2,3 }, new int[] { 4,3,2,3 })]
        public void Knapsack_TestWithFourItems(int[] weights, int[] lengths){
            var cost = 6;
            Job[] jobs = new Job[weights.Length];
            for(var i = 0;i<weights.Length;i++){
                jobs[i] = new Job(weights[i], lengths[i]);
            }
            var value = knapsack(jobs.ToArray(), cost);
            Debug.WriteLine($"The total value (in length) of the items in the knapsack is {value}.");
            Debug.WriteLine($"The bag can only carry {cost}");
        }
        public int knapsack(Job[] input, int Capacity)
        {
            // subproblem solutions
            int[, ] matrix = new int[input.Length+1, Capacity+1];

            // base case
            for(var c = 0;c<=Capacity;c++)
            {
                matrix[0, c] = 0;
            }

            // systematically solve all subproblems
            for(var i = 1;i<=input.Length;i++) {
                for(var c = 0;c<=Capacity;c++) {
                    // use recurrence from Corollary 16.5
                    if (input[i-1].Cost() > c) {
                        matrix[i, c] = matrix[i-1, c];
                    } else {
                        var case1 = matrix[i-1, c];
                        var case2 = matrix[i-1, c-input[i-1].Cost()] + input[i-1].Value();
                        matrix[i,c] = Math.Max(case1, case2);
                    }
                }
            }
            return matrix[input.Length, Capacity]; //solution to largest subproblem
        }
    }
}
