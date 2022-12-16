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
        public static int FindNthSmallestNumber(this IList<int> list, int Nth) 
        {
            int start = 0;
            int end = list.Count;
            return FindNthSmallestNumber(list, Nth, start, end);
        }
        public static int MediumOfMediums(this IList<int> list)
        {
            var n = list.Count;
            if(list.Count == 1)
                return list[0];
            var chunkSize = (int)Math.Ceiling((double)n/5);
            var C = new int[chunkSize];
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
        public static int ChoosePivot(this IList<int> list, int start, int end)
        {
            var tmp = list.Skip(start).Take(end-start).ToList();
            var median = MediumOfMediums(tmp);
            return median;
        }
        
        public static int FindNthSmallestNumber(this IList<int> list, int Nth, int start, int end)
        {
            if (start >= end)
                return list[0];
            var pivotLocation = ChoosePivot(list, start, end);
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