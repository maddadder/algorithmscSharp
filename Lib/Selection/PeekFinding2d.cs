using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Selection
{
    public class PeekFinding2d
    {

        public static int[] PeekAny<T>(T[,] input) where T : IComparable
        {
            var n = input.Length;
            return PeekAny(input, 0, n-1, n);
        }
        public static int[] PeekAny<T>(T[,] input, int low, int high, int n) where T : IComparable
        {
            int[] result = new int[2];
            int row = input.GetLength(0);
            int column = input.GetLength(1);
            for (int i = 0; i < row; i++) {
                for (int j = 0; j < column; j++) {

                    // checking with top element
                    if (i > 0)
                        if (input[i,j].CompareTo(input[i - 1,j]) < 0)
                            continue;

                    // checking with right element
                    if (j < column - 1)
                        if (input[i,j].CompareTo(input[i,j + 1]) < 0)
                            continue;

                    // checking with bottom element
                    if (i < row - 1)
                        if (input[i,j].CompareTo(input[i + 1,j]) < 0)
                            continue;

                    // checking with left element
                    if (j > 0)
                        if (input[i,j].CompareTo(input[i,j - 1]) < 0)
                            continue;
                    result[0] = i;
                    result[1] = j;
                    break;
                }
            }
            return result;
        }
    }
}