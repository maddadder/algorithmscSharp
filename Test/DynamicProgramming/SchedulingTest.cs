using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Model;

namespace Test.DynamicProgramming
{
    
    [TestClass]
    public class SchedulingTest
    {
        [TestMethod]
        public void CalculateWeightedSumUsingDifference()
        {
            var jobs = new List<Item>();
            jobs.Add(new Item(3,5));
            jobs.Add(new Item(1,2));

            Item[] sortedJobs = jobs.OrderByDescending(j => (j.Weight - j.Size)).ThenByDescending(j => j.Weight).ToArray();

            long weightedSum = 0;

            long totalTimePassed = 0;
            for (int i = 0; i < sortedJobs.Length; i++)
            {
                Item currentJob = sortedJobs[i];
                totalTimePassed += currentJob.Size;
                weightedSum += currentJob.Weight * totalTimePassed;
            }
            Debug.WriteLine(weightedSum);
        }
        [TestMethod]
        public void CalculateWeightedSumUsingRatio()
        {
            var jobs = new List<Item>();
            jobs.Add(new Item(3,5));
            jobs.Add(new Item(1,2));
            Item[] sortedJobs = jobs.OrderByDescending(j => ((double)j.Weight) / j.Size).ToArray();

            long weightedSum = 0;

            long totalTimePassed = 0;
            for (int i = 0; i < sortedJobs.Length; i++)
            {
                Item currentJob = sortedJobs[i];
                totalTimePassed += currentJob.Size;
                weightedSum += currentJob.Weight * totalTimePassed;
            }
            Debug.WriteLine(weightedSum);
        }

    }
}
