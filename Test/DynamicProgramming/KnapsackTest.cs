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
            var ItemsInHouse = 1000;
            int cost = ItemsInHouse*2;
            for(var i = 0;i<ItemsInHouse;i++){
                jobs.Add(new Job(rand.Next(1,ItemsInHouse),rand.Next(1,ItemsInHouse)));
            }
            Tuple<Job[], int>[, ] matrix = new Tuple<Job[], int>[ItemsInHouse + 1, ItemsInHouse*2 + 1];
            var result = knapsack(new Tuple<Job[], int>(jobs.ToArray(), cost), matrix);
            var sumSubset = result.Item1.Sum(x => x.Cost());
            Debug.WriteLine($"The cost (in space) of the items in the knapsack is {sumSubset}.");
            Debug.WriteLine($"The bag can only carry {cost}");
        }
        ///Returns a tuple of total value, and the items
        public Tuple<Job[], int> knapsack(Tuple<Job[],int> input, Tuple<Job[], int>[, ] matrix)
        {
            Job[] toConsider = input.Item1;
            int n = toConsider.Count();
            int avail = input.Item2;

            //base case
            if (matrix[n, avail] != null)
            {
                return matrix[n, avail];
            } 
            else if(toConsider.Length == 0 || avail == 0)
            {
                return new Tuple<Job[], int>(new Job[]{}, 0);
            }
            else if(toConsider[0].Cost() > avail)
            {
                //explore right branch only
                return knapsack(new Tuple<Job[], int>(toConsider.Skip(1).ToArray(), avail), matrix);
            }
            else
            {
                Job nextItem = toConsider[0];
                //explore left branch
                Tuple<Job[], int> with = knapsack(new Tuple<Job[], int>(toConsider.Skip(1).ToArray(), avail - nextItem.Cost()), matrix);
                Job[] withToTake = with.Item1;
                int withVal = with.Item2;
                withVal += nextItem.Value();
                //explore right branch
                var without = knapsack(new Tuple<Job[], int>(toConsider.Skip(1).ToArray(), avail), matrix);
                var withoutToTake = without.Item1;
                var withoutVal = without.Item2;
                Tuple<Job[], int> result = null;
                //Choose better branch
                if(withVal > withoutVal)
                {
                    result = new Tuple<Job[], int>(withToTake.Concat(new Job[]{nextItem}).ToArray(), withVal);
                }
                else
                {
                    result = new Tuple<Job[], int>(withoutToTake, withoutVal);
                }
                matrix[n,avail] = result;
                return result;
            }
        }
    }
}
