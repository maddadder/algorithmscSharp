using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Selection
{
    public class PeekFinding
    {

        public static int PeekAny<T>(T[] input) where T : IComparable
        {
            var n = input.Length;
            return PeekAny(input, 0, n-1, n);
        }
        public static int PeekAny<T>(T[] input, int low, int high, int n) where T : IComparable
        {

            // get middle element from low and high
            int mid = low + (high - low) / 2;

            // if (mid == 0 or mid item is larger than left) AND (mid is EOF or mid is greater than right)
            if ((mid == 0 || input[mid].CompareTo(input[mid - 1]) >= 0) && (mid == n - 1 || input[mid].CompareTo(input[mid + 1]) >= 0))
                return mid;
            
            // if left element is greater then scroll left
            else if(mid > 0 && input[mid-1].CompareTo(input[mid]) > 0){
                return PeekAny(input, low, mid-1, n);
            }
            else
            {
                //else scroll right
                return PeekAny(input, mid+1, high, n);
            }
        }
        public static int[] PeekAll<T>(T[] input, int n) where T : IComparable
        {
            List<int> peaks = new List<int>();
            //base cases
            if (n == 1)
                peaks.Add(0);
            if (input[0].CompareTo(input[1]) >= 0)
                peaks.Add(0);
            if (input[n - 1].CompareTo(input[n - 2]) >= 0)
                peaks.Add(n-1);
            
            // Check for every other element
            for(int i = 1; i < n - 1; i++)
            {
                // Check if the neighbors are smaller
                if (input[i].CompareTo(input[i - 1]) >= 0 &&
                    input[i].CompareTo(input[i + 1]) >= 0)
                    peaks.Add(i);
            }
            return peaks.ToArray();
        }
        public static int PeekAnyLinear<T>(T[] input, int n) where T : IComparable
        {
            //base cases
            if (n == 1)
                return 0;
            if (input[0].CompareTo(input[1]) >= 0)
                return 0;
            if (input[n - 1].CompareTo(input[n - 2]) >= 0)
                return n-1;
            
            // Check for every other element
            for(int i = 1; i < n - 1; i++)
            {
                // Check if the neighbors are smaller
                if (input[i].CompareTo(input[i - 1]) >= 0 &&
                    input[i].CompareTo(input[i + 1]) >= 0)
                    return i;
            }
            throw new NotImplementedException("This should not happen");
        }
    }
}