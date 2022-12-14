using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Extensions;
using Models;

namespace Lib.Selection
{
    public static class ClosestPair
    {
        public static T1 FindClosestPair<T1, T2>(IEnumerable<T2> input) 
        where T1 : Point2dPair, IComparable
        where T2 : Point2d, IComparable
        {
            var inputSortedX = input.OrderBy(x => x.X).Select(x => x);
            var inputSortedY = input.OrderBy(x => x.Y).Select(x => x);
            return SolveRecursive<T1,T2>(inputSortedX.ToArray(), inputSortedY.ToArray(), input.Count());
        }
        private static T1 SolveRecursive<T1, T2>(T2[] x, T2[] y, int n) 
        where T1 : Point2dPair, IComparable
        where T2 : Point2d, IComparable
        {
            if (n == 0)
                throw new Exception("list is empty");
            if (n < 4)
                return FindClosestPairBruteForce<T1,T2>(x, int.MaxValue);

            var mid = n/2;
            var Left_x = x.Slice(0,mid);
            var Left_y = y.Slice(0,mid);
            var Right_x = x.Slice(mid,n-mid);
            var Right_y = y.Slice(mid,n-mid);
            var closestLeftPair = SolveRecursive<T1,T2>(Left_x, Left_y, mid);
            var closestRightPair = SolveRecursive<T1,T2>(Right_x, Right_y, n - mid);
            T1 closestPair = default(T1);
            if (closestLeftPair.CompareTo(closestRightPair) < 0)
            {
                closestPair = closestLeftPair;
            }
            else
            {
                closestPair = closestRightPair;
            }
            T1 closestSplitPair = ClosestSplitPair<T1,T2>(Left_y, Right_y, closestPair.DistanceSquared());
            if (closestSplitPair.CompareTo(closestPair) < 0)
            {
                closestPair = closestSplitPair;
            }
            return closestPair;
        }
        private static T1 ClosestSplitPair<T1, T2>(T2[] Left_y, T2[] Right_y, double minDistance)
        where T1 : Point2dPair, IComparable
        where T2 : Point2d, IComparable
        {
            var midPoint = Left_y[Left_y.Length-1];
            var SplitPair_y = new List<T2>();
            var leftside = Math.Abs(midPoint.X - minDistance) * 2;
            for(var k = 0;k<Left_y.Length;k++)
                if (Math.Abs(Left_y[k].X - midPoint.X) <= leftside) 
                    SplitPair_y.Add(Left_y[k]);
            for(var k = 0;k<Right_y.Length;k++)
                if (Math.Abs(Right_y[k].X - midPoint.X) <= leftside) 
                    SplitPair_y.Add(Right_y[k]);
            return FindClosestPairBruteForce<T1,T2>(SplitPair_y.ToArray(),7);
        }
        public static T1 FindClosestPairBruteForce<T1, T2>(T2[] input, int MaxSearchDepth = int.MaxValue)
        where T1 : Point2dPair, IComparable
        where T2 : Point2d, IComparable
        {
            int n = input.Length;
            if(n == 0){
                return null;
            }
            if(n == 1){
                return (T1)new Point2dPair(input[0], new Point2d(float.MaxValue,float.MaxValue));
            }
            var query =  (from i in Enumerable.Range( 0, n-1)
                          from j in Enumerable.Range( i+1, Math.Min(MaxSearchDepth,n-(i+1)))
                          select new Point2dPair(input[i],input[j]));

            var result = (from segment in query
                         orderby segment
                         select segment).First();

            return (T1)result;
        }
    }
}