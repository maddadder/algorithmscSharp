using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Lib.Sorting
{

    public static class ListOperations
    {
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