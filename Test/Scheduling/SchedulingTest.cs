using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Sorting;
using Lib.Selection;

namespace Test.Scheduling
{
    public class Job
    {
        public Job(int weight, int length)
        {
            Weight = weight;
            Length = length;
        }

        public int Length { get; private set; }
        public int Weight { get; private set; }
    }
    [TestClass]
    public class SchedulingTest
    {
        [TestMethod]
        public void CalculateWeightedSumUsingDifference()
        {
            var jobs = new List<Job>();
            jobs.Add(new Job(3,5));
            jobs.Add(new Job(1,2));

            Job[] sortedJobs = jobs.OrderByDescending(j => (j.Weight - j.Length)).ThenByDescending(j => j.Weight).ToArray();

            long weightedSum = 0;

            long totalTimePassed = 0;
            for (int i = 0; i < sortedJobs.Length; i++)
            {
                Job currentJob = sortedJobs[i];
                totalTimePassed += currentJob.Length;
                weightedSum += currentJob.Weight * totalTimePassed;
            }
            Debug.WriteLine(weightedSum);
        }
        [TestMethod]
        public void CalculateWeightedSumUsingRatio()
        {
            var jobs = new List<Job>();
            jobs.Add(new Job(3,5));
            jobs.Add(new Job(1,2));
            Job[] sortedJobs = jobs.OrderByDescending(j => ((double)j.Weight) / j.Length).ToArray();

            long weightedSum = 0;

            long totalTimePassed = 0;
            for (int i = 0; i < sortedJobs.Length; i++)
            {
                Job currentJob = sortedJobs[i];
                totalTimePassed += currentJob.Length;
                weightedSum += currentJob.Weight * totalTimePassed;
            }
            Debug.WriteLine(weightedSum);
        }

    }
}
