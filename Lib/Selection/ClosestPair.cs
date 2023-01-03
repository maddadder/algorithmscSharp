using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Extensions;
using Lib.Model;

namespace Lib.Selection
{
    public static class SmallestLineSegment
    {
        public static LineSegment FindSmallestLineSegment(IEnumerable<Vector2> input) 
        {
            var inputSortedX = input.OrderBy(x => x.X).Select(x => x);
            var inputSortedY = input.OrderBy(x => x.Y).Select(x => x);
            return SolveRecursive(inputSortedX.ToArray(), inputSortedY.ToArray(), input.Count());
        }
        private static LineSegment SolveRecursive(Vector2[] x, Vector2[] y, int n) 
        {
            if (n == 0)
                throw new Exception("list is empty");
            if (n < 4)
                return FindSmallestLineSegmentBruteForce(x, int.MaxValue);

            var mid = n/2;
            var Left_x = x.Slice(0,mid);
            var Left_y = y.Slice(0,mid);
            var Right_x = x.Slice(mid,n-mid);
            var Right_y = y.Slice(mid,n-mid);
            var closestLeftPair = SolveRecursive(Left_x, Left_y, mid);
            var closestRightPair = SolveRecursive(Right_x, Right_y, n - mid);
            LineSegment closestPair = null;
            if (closestLeftPair.CompareTo(closestRightPair) < 0)
            {
                closestPair = closestLeftPair;
            }
            else
            {
                closestPair = closestRightPair;
            }
            LineSegment closestSplitPair = ClosestSplitPair(Left_y, Right_y, closestPair.DistanceSquared());
            if (closestSplitPair.CompareTo(closestPair) < 0)
            {
                closestPair = closestSplitPair;
            }
            return closestPair;
        }
        private static LineSegment ClosestSplitPair(Vector2[] Left_y, Vector2[] Right_y, double minDistance)
        {
            var midPoint = Left_y[Left_y.Length-1];
            var SplitPair_y = new List<Vector2>();
            var leftside = Math.Abs(midPoint.X - minDistance) * 2;
            for(var k = 0;k<Left_y.Length;k++)
                if (Math.Abs(Left_y[k].X - midPoint.X) <= leftside) 
                    SplitPair_y.Add(Left_y[k]);
            for(var k = 0;k<Right_y.Length;k++)
                if (Math.Abs(Right_y[k].X - midPoint.X) <= leftside) 
                    SplitPair_y.Add(Right_y[k]);
            return FindSmallestLineSegmentBruteForce(SplitPair_y.ToArray(),7);
        }
        public static LineSegment FindSmallestLineSegmentBruteForce(Vector2[] input, int MaxSearchDepth = int.MaxValue)
        {
            int n = input.Length;
            if(n == 0){
                return null;
            }
            if(n == 1){
                return (LineSegment)new LineSegment(input[0], new Vector2(float.MaxValue,float.MaxValue));
            }
            LineSegment closestPair = new LineSegment(new Vector2(float.MinValue,float.MinValue), new Vector2(float.MaxValue,float.MaxValue));
            for(var i = 0;i<n-1;i++)
            {
                for(var j = i+1;j<Math.Min(n,MaxSearchDepth);j++)
                {
                    LineSegment potential = new LineSegment(input[i],input[j]);
                    if(closestPair.CompareTo(potential) < 0)
                    {
                        closestPair = new LineSegment(input[i],input[j]);
                    }
                }
            }
            
            return closestPair;
        }
    }
}