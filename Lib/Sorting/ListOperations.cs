using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
            int n = input.Length;
            if (n == 0)
                return input;
            if (n == 1)
                return input;
            if (n == 2)
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
            
            var left = input.Slice(0,n/2);
            var right = input.Slice(n/2,(int)Math.Ceiling(n/(double)2));
            var c = SplitAndSort(left);
            var d = SplitAndSort(right);
            return Merge(c,d,n);
        }
        private static int[] Merge(int[] left, int[] right, int n)
        {
            int i = 0;
            int j = 0;
            int[] result = new int[n];
            left = left.Add(int.MaxValue);
            right = right.Add(int.MaxValue);

            for(var k = 0;k<n;k++)
            {
                if(left[i] < right[j])
                {
                    result[k] = left[i];
                    i+=1;
                }
                else
                {
                    result[k] = right[j];
                    j+=1;
                }
            }
            return result;
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