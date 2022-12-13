using System;
using Extensions;

namespace Lib.Sorting
{
    public static class ListOperations
    {
        public static int[] MergeSort(int[] input)
        {
            return SplitAndSort(input);
        }
        private static int[] SplitAndSort(int[] input)
        {
            int inputSize = input.Length;
            if (inputSize == 0)
                return input;
            if (inputSize == 1)
                return input;
            if (inputSize == 2)
            {
                if (input[0] < input[1])
                {
                    return input;
                }
                else
                {
                    return new int[2]{input[1], input[0]};
                }
            }
            
            var left = input.Slice(0,inputSize/2);
            var right = input.Slice(inputSize/2,(int)Math.Ceiling(inputSize/(double)2));
            left = SplitAndSort(left);
            right = SplitAndSort(right);
            return Merge(left,right,inputSize);
        }
        private static int[] Merge(int[] left, int[] right, int inputSize)
        {
            int i = 0;
            int j = 0;
            int[] combined = new int[inputSize];
            left = left.Add(int.MaxValue);
            right = right.Add(int.MaxValue);

            for(var k = 0;k<inputSize;k++)
            {
                if(left[i] < right[j])
                {
                    combined[k] = left[i];
                    i+=1;
                }
                else
                {
                    combined[k] = right[j];
                    j+=1;
                }
            }
            return combined;
        }
        public static string Reverse(string input)
        {
            char[] output = input.ToCharArray();
            int length = input.Length;
            int mid = length/2;
            int len = length - 1;
            for(var i=0;i<mid;i++)
            {
                char left = input[i];
                char right = input[len-i];
                output[i] = right;
                output[len-i] = left;
            }
            return new string(output);
        }
    }
}