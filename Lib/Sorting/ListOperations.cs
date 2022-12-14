using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Sorting
{
    public static class ListOperations
    {
        public static IEnumerable<T> MergeSort<T>(IEnumerable<T> input) where T : IComparable
        {
            var array = input.ToArray();
            return SplitRecursive(array, array.Length);
        }
        
        private static T[] SplitRecursive<T>(T[] input, int inputSize) where T : IComparable
        {
            if (inputSize <= 1)
                return input;
            var leftLength = inputSize/2;
            var rightLength = inputSize - leftLength;
            T[] left = new T[leftLength];
            T[] right = new T[rightLength];
            Array.Copy(input, left, leftLength);
            Array.Copy(input, leftLength, right, 0, rightLength);
            SplitRecursive(left, leftLength);
            SplitRecursive(right, rightLength);
            return Merge(input, left, right, inputSize, leftLength, rightLength);
        }
        private static T[] Merge<T>(T[] input, T[] left, T[] right, int inputSize, int leftLength, int rightLength) where T : IComparable
        {
            var leftIndex = 0;
            var rightIndex = 0;
            for(var k = 0;k<inputSize;k++)
            {
                if(leftIndex == leftLength)
                {
                    input[k] = right[rightIndex];
                    rightIndex += 1;
                }
                else if(rightIndex == rightLength)
                {
                    input[k] = left[leftIndex];
                    leftIndex += 1;
                }
                else if(left[leftIndex].CompareTo(right[rightIndex]) < 0)
                {
                    input[k] = left[leftIndex];
                    leftIndex += 1;
                }
                else
                {
                    input[k] = right[rightIndex];
                    rightIndex += 1;
                }
            }
            return input;
        }
        public static IEnumerable<T> Reverse<T>(IEnumerable<T> input)
        {
            var inputArray = input.ToArray();
            int length = inputArray.Length;
            T[] output = new T[length];
            int mid = length/2;
            int len = length - 1;
            for(var i=0;i<mid;i++)
            {
                var left = inputArray[i];
                var right = inputArray[len-i];
                output[i] = right;
                output[len-i] = left;
            }
            return output;
        }
    }
}