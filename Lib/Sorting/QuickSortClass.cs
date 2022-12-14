using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Sorting
{
    public static class QuickSortClass
    {
        public static void QuickSort<T>(T[] input) where T : IComparable
        {
            Random random = new Random();
            SolveRecursive(input, 0, input.Length, random);
        }
        private static void SolveRecursive<T>(T[] input, int left, int right, Random random) where T : IComparable
        {
            if (left >= right)
                return;
            var i = random.Next(left,right-1);
            Swap(input, left, i);
            var j = Partition<T>(input, left, right);
            SolveRecursive(input,left, j, random);
            SolveRecursive(input,j+1,right, random);
        }
        private static void Swap<T>(T[] input, int left, int right)
        {
            var temp = input[left];
            input[left] = input[right];
            input[right] = temp;
        }
        private static int Partition<T>(T[] input, int left, int right) where T : IComparable
        {
            var pivot = input[left];
            var i = left + 1;
            for(var j=left+1;j<right;j++){
                if (input[j].CompareTo(pivot) < 0){
                    Swap(input, j, i);
                    i += 1;
                }
            }
            Swap(input, left, i-1);
            return i-1;
        }
    }
}