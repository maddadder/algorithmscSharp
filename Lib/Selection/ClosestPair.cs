using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Extensions;

namespace Lib.Selection
{
    public static class ClosestPair
    {
        public static Tuple<double,Vector2[]> FindClosestPair(Vector2[] input)
        {
            var inputSortedX = input.OrderBy(x => x.X).Select(x => x);
            var inputSortedY = input.OrderBy(x => x.Y).Select(x => x);
            return SolveRecursive(inputSortedX.ToArray(), inputSortedY.ToArray(), input.Length);
        }
        private static Tuple<double,Vector2[]> SolveRecursive(Vector2[] x, Vector2[] y, int n)
        {
            if (n == 0)
                throw new Exception("list is empty");
            if (n < 4)
                return FindClosestPairBruteForce(x, int.MaxValue);

            var mid = n/2;
            var Left_x = x.Slice(0,mid);
            var Left_y = y.Slice(0,mid);
            var Right_x = x.Slice(mid,n-mid);
            var Right_y = y.Slice(mid,n-mid);
            var closestLeftPair = SolveRecursive(Left_x, Left_y, mid);
            var clostestRightPair = SolveRecursive(Right_x, Right_y, n - mid);
            Vector2[] closestPair = null;
            double minDistance = double.MaxValue;
            if (closestLeftPair.Item1 < clostestRightPair.Item1)
            {
                closestPair = closestLeftPair.Item2;
                minDistance = closestLeftPair.Item1;
            }
            else
            {
                closestPair = clostestRightPair.Item2;
                minDistance = clostestRightPair.Item1;
            }
            var closestSplitPair = ClosestSplitPair(Left_y, Right_y, minDistance);
            if (closestSplitPair.Item1 < minDistance)
            {
                closestPair = closestSplitPair.Item2;
                minDistance = closestSplitPair.Item1;
            }
            return new Tuple<double,Vector2[]>(minDistance,closestPair);
        }
        private static Tuple<double,Vector2[]> ClosestSplitPair(Vector2[] Left_y, Vector2[] Right_y, double minDistance){
            var midPoint = Left_y[Left_y.Length-1];
            var SplitPair_y = new List<Vector2>();
            var leftside = Math.Abs(midPoint.X - minDistance) * 2;
            for(var k = 0;k<Left_y.Length;k++)
                if (Math.Abs(Left_y[k].X - midPoint.X) <= leftside) 
                    SplitPair_y.Add(Left_y[k]);
            for(var k = 0;k<Right_y.Length;k++)
                if (Math.Abs(Right_y[k].X - midPoint.X) <= leftside) 
                    SplitPair_y.Add(Right_y[k]);
            return FindClosestPairBruteForce(SplitPair_y.ToArray(),7);
        }
        public static Tuple<double,Vector2[]> FindClosestPairBruteForce( Vector2[] input, int MaxSearchDepth = int.MaxValue )
        {
            int n = input.Length;
            if(n < 2){
                return new Tuple<double,Vector2[]>(double.MaxValue, input);
            }
            var result = Enumerable.Range( 0, n-1)
                .SelectMany( i => Enumerable.Range( i+1, Math.Min(MaxSearchDepth,n-(i+1)) )
                    .Select( j => new Vector2[2]{ input[i], input[j] }))
                    .OrderBy( seg => Vector2.DistanceSquared(seg[0], seg[1]) )
                    .First();

            return new Tuple<double,Vector2[]>(Vector2.DistanceSquared(result[0], result[1]), result);
        }
    }
}