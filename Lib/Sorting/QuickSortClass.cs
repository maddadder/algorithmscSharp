using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Sorting
{
    public static class QuickSortClass
    {
        public static void QuickSort<T>(IList<T> input) where T : IComparable
        {
            Random random = new Random();
            SolveRecursive(input, 0, input.Count, random);
        }
        private static void SolveRecursive<T>(IList<T> input, int start, int end, Random random) where T : IComparable
        {
            if (start >= end)
                return;
            var pivotLocation = random.Next(start,end);
            var pivotIndex = Partition(input, pivotLocation, start, end);
            SolveRecursive(input,start, pivotIndex, random);
            SolveRecursive(input,pivotIndex+1,end, random);
        }
        public static void Swap<T>(IList<T> input, int start, int end)
        {
            var temp = input[start];
            input[start] = input[end];
            input[end] = temp;
        }
        public static int Partition<T>(IList<T> input, int pivotLocation, int start, int end) where T : IComparable
        {
            Swap(input, start, pivotLocation); //make pivot the first item in list
            T pivot = input[start]; //the pivot boundary
            //invariant:
            //increment j such that each element between i and j is less than the pivot boundary.
            //i and j should delineate a boundary between processsed elements less than and greater than the pivot
            int i = start + 1; 
            for(int j=i;j<end;j++){
                if (input[j].CompareTo(pivot) < 0){
                    Swap(input, j, i); //put j behind the pivot boundary
                    i += 1; //restores invariant
                }
            }
            Swap(input, start, i-1); //restore original swap
            return i-1; //report final pivot position
        }
    }
}