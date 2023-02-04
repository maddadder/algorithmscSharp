using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Extensions;

namespace Lib.Graphs
{
    

    public partial class MathGraph<T> where T : IComparable<T>
    {
        public T CalculateMWISFrom(T start)
        {
            var nodes = FindAccessibleVertices(start).ToArray();
            var results = new T[nodes.Length + 1];
            results[0] = default;
            results[1] = nodes[0];
            for(var i = 2;i<=nodes.Length;i++){
                var s2 = Add(results[i-2], nodes[i-1]);
                var s1 = results[i-1];
                results[i] = s1.CompareTo(s2) > 0 ? s1 : s2;
            }
            //CalculateUsedNodeIDs();
            return results[nodes.Length];
        }
    }
}
