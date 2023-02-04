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
        public SortedDictionary<T, float> Dijkstra(T left)
        {
            ComponentWeights = SetAllVertexDistances();
            parent = SetAllVertexParents();
            SortedDictionary<T, bool> isVertexVisited = ClearAllVertexMarks();
            ComponentWeights[left] = 0;
            PriorityQueue<T,float> indexNDistance = new PriorityQueue<T,float>();
            indexNDistance.Enqueue(left, 0);
            while (indexNDistance.Count > 0) {
                left = indexNDistance.Dequeue();
                isVertexVisited[left] = true;
                foreach (var right in Vertices[left].OutEdge) {
                    var currentCalculatedDist = ComponentWeights[left] + right.EdgeWeight;
                    if (isVertexVisited[right.dest.Component] == false && 
                        ComponentWeights[right.dest.Component].CompareTo(currentCalculatedDist) > 0) 
                    {
                        ComponentWeights[right.dest.Component] = currentCalculatedDist;
                        parent[right.dest.Component] = left;
                        indexNDistance.Enqueue(right.dest.Component, currentCalculatedDist);
                    }
                }
            }
            return ComponentWeights;
        }
    }
}
