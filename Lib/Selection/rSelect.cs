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
            while (true)
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
                    end = pivotIndex;
                }
                else 
                {
                    start = pivotIndex+1;
                }
            }
        }
    }
}