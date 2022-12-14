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
        public static TOutput FindClosestPair<TInput, TOutput>(IEnumerable<TInput> input) 
        where TInput : Point2d, IComparable
        where TOutput : Point2dPair, IComparable
        
        {
            var inputSortedX = input.OrderBy(x => x.X).Select(x => x);
            var inputSortedY = input.OrderBy(x => x.Y).Select(x => x);
            return SolveRecursive<TInput,TOutput>(inputSortedX.ToArray(), inputSortedY.ToArray(), input.Count());
        }
        private static TOutput SolveRecursive<TInput, TOutput>(TInput[] x, TInput[] y, int n) 
        where TInput : Point2d, IComparable
        where TOutput : Point2dPair, IComparable
        
        {
            if (n == 0)
                throw new Exception("list is empty");
            if (n < 4)
                return FindClosestPairBruteForce<TInput, TOutput>(x, int.MaxValue);

            var mid = n/2;
            var Left_x = x.Slice(0,mid);
            var Left_y = y.Slice(0,mid);
            var Right_x = x.Slice(mid,n-mid);
            var Right_y = y.Slice(mid,n-mid);
            var closestLeftPair = SolveRecursive<TInput,TOutput>(Left_x, Left_y, mid);
            var closestRightPair = SolveRecursive<TInput,TOutput>(Right_x, Right_y, n - mid);
            TOutput closestPair = default(TOutput); // TOutput closestPair = null
            if (closestLeftPair.CompareTo(closestRightPair) < 0)
            {
                closestPair = closestLeftPair;
            }
            else
            {
                closestPair = closestRightPair;
            }
            TOutput closestSplitPair = ClosestSplitPair<TInput,TOutput>(Left_y, Right_y, closestPair.DistanceSquared());
            if (closestSplitPair.CompareTo(closestPair) < 0)
            {
                closestPair = closestSplitPair;
            }
            return closestPair;
        }
        private static TOutput ClosestSplitPair<TInput,TOutput>(TInput[] Left_y, TInput[] Right_y, double minDistance)
        where TInput : Point2d, IComparable
        where TOutput : Point2dPair, IComparable
        
        {
            var midPoint = Left_y[Left_y.Length-1];
            var SplitPair_y = new List<TInput>();
            var leftside = Math.Abs(midPoint.X - minDistance) * 2;
            for(var k = 0;k<Left_y.Length;k++)
                if (Math.Abs(Left_y[k].X - midPoint.X) <= leftside) 
                    SplitPair_y.Add(Left_y[k]);
            for(var k = 0;k<Right_y.Length;k++)
                if (Math.Abs(Right_y[k].X - midPoint.X) <= leftside) 
                    SplitPair_y.Add(Right_y[k]);
            return FindClosestPairBruteForce<TInput,TOutput>(SplitPair_y.ToArray(),7);
        }
        public static TOutput FindClosestPairBruteForce<TInput,TOutput>(TInput[] input, int MaxSearchDepth = int.MaxValue)
        where TInput : Point2d, IComparable
        where TOutput : Point2dPair, IComparable
        {
            int n = input.Length;
            if(n == 0){
                return null;
            }
            if(n == 1){
                return (TOutput)new Point2dPair(input[0], new Point2d(float.MaxValue,float.MaxValue));
            }
            Point2dPair closestPair = new Point2dPair(new Point2d(float.MinValue,float.MinValue), new Point2d(float.MaxValue,float.MaxValue));
            for(var i = 0;i<n-1;i++)
            {
                for(var j = i+1;j<Math.Min(n,MaxSearchDepth);j++)
                {
                    Point2dPair potential = new Point2dPair(input[i],input[j]);
                    if(closestPair.CompareTo(potential) < 0)
                    {
                        closestPair = new Point2dPair(input[i],input[j]);
                    }
                }
            }
            
            return (TOutput)closestPair;
        }
    }
}