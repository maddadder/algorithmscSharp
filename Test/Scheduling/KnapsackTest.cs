using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Model;

namespace Test.Scheduling
{
    
    [TestClass]
    public class KnapsackTest
    {
        
        [TestMethod]
        public void Knapsack_Test(){
            var jobs = new List<Job>();
            jobs.Add(new Job(3,5));
            jobs.Add(new Job(1,2));
            var result = knapsack(jobs.ToArray(), 4);
            Debug.WriteLine(result.Item1);
            foreach(var row in result.Item2){
                Debug.WriteLine($"{row.Weight},{row.Length}");
            }
        }
        ///Returns a tuple of total value, and the items
        public Tuple<int,Job[]> knapsack(Job[] toConsider, int avail)
        {
            //base case
            if(toConsider.Length == 0 || avail == 0)
            {
                return new Tuple<int, Job[]>(0, new Job[]{});
            }
            else if(toConsider[0].Cost() > avail){
                //explore right branch only
                return knapsack(toConsider.Skip(1).ToArray(), avail);
            }
            else
            {
                var nextItem = toConsider[0];
                //explore left branch
                var with = knapsack(toConsider.Skip(1).ToArray(), avail - nextItem.Cost());
                var withVal = with.Item1;
                var withToTake = with.Item2;
                withVal += nextItem.Value();
                //explore right branch
                var without = knapsack(toConsider.Skip(1).ToArray(), avail);
                var withoutVal = without.Item1;
                var withoutToTake = without.Item2;
                //Choose better branch
                if(withVal > withoutVal){
                    return new Tuple<int, Job[]>(withVal, withToTake.Concat(new Job[]{nextItem}).ToArray());
                }
                else
                {
                    return new Tuple<int, Job[]>(withoutVal, withoutToTake);
                }
            }
        }
    }
}
