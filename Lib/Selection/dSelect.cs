using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Extensions;
using Lib.Sorting;

namespace Lib.Selection
{
    public static class dSelect
    {
        public static T FindNthSmallestNumber<T>(this IList<T> list, int Nth) where T : IComparable
        {
            int start = 0;
            int end = list.Count;
            return FindNthSmallestNumber(list, Nth, start, end);
        }
        public static T MediumOfMediums<T>(this IList<T> list) where T : IComparable
        {
            var n = list.Count;
            if(list.Count == 1)
                return list[0];
            var chunkSize = (int)Math.Ceiling((double)n/5);
            var C = new T[chunkSize];
            var Ci = 0;
            var chunks = ListExtensions.SplitByChunks(list,5);
            foreach(var chunk in chunks)
            {
                var lst = chunk.ToList();
                var mid = lst.Count/2;
                C[Ci] = lst[mid];
                Ci += 1;
            }
            return MediumOfMediums(C);
        }
        public static T ChoosePivot<T>(this IList<T> list, int start, int end) where T : IComparable
        {
            var tmp = list.Skip(start).Take(end-start).ToList();
            var median = MediumOfMediums(tmp);
            return median;
        }
        
        public static T FindNthSmallestNumber<T>(this IList<T> list, int Nth, int start, int end) where T : IComparable
        {
            if (start >= end)
                return list[0];
            var pivot = ChoosePivot(list, start, end);
            
            //this requires the list type is an int
            var pivotLocation = Convert.ToInt32(pivot);
            var pivotIndex = QuickSortClass.Partition(list,pivotLocation,start,end);
            if(pivotIndex == Nth)
            {
                return list[pivotIndex];
            }
            else if(pivotIndex > Nth)
            {
                return FindNthSmallestNumber(list, Nth, start, pivotIndex);
            }
            else 
            {
                return FindNthSmallestNumber(list, Nth, pivotIndex+1, end);
            }
        }
    }
}