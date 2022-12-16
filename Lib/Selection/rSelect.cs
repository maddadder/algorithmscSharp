using System;
using System.Collections.Generic;
using Lib.Sorting;

namespace Lib.Selection
{
    public static class rSelect
    {
        public static T FindNthSmallestNumber<T>(this IList<T> list, int Nth) where T : IComparable
        {
            var rnd = new Random();
            int start = 0;
            int end = list.Count;
            return FindNthSmallestNumber(list, Nth, start, end, rnd);
        }
        public static T FindNthSmallestNumber<T>(this IList<T> list, int Nth, int start, int end, Random rnd) where T : IComparable
        {
            if (start >= end)
                return list[0];
            var pivotLocation = rnd.Next(start,end); //this range should get smaller and smaller
            var pivotIndex = QuickSortClass.Partition(list,pivotLocation,start,end);
            if(pivotIndex == Nth)
            {
                return list[pivotIndex]; //you got lucky
            }
            else if(pivotIndex > Nth)
            {
                return FindNthSmallestNumber(list, Nth, start, pivotIndex, rnd);
            }
            else 
            {
                return FindNthSmallestNumber(list, Nth, pivotIndex+1, end, rnd);
            }
        }
    }
}