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
        public SortedDictionary<T, float> Dijkstra(T src)
        {
            ComponentWeights = SetAllVertexDistances();
            parent = SetAllVertexParents();
            SortedDictionary<T, bool> isVertexVisited = ClearAllVertexMarks();
            ComponentWeights[src] = 0;
            PriorityQueue<T,float> indexNDistance = new PriorityQueue<T,float>();
            indexNDistance.Enqueue(src, 0);
            while (indexNDistance.Count > 0) {
                src = indexNDistance.Dequeue();
                isVertexVisited[src] = true;
                foreach (var dest in Vertices[src].OutEdge) {
                    var currentCalculatedDist = ComponentWeights[src] + dest.Key.EdgeWeight;
                    if (isVertexVisited[dest.Key.dest.Component] == false && 
                        ComponentWeights[dest.Key.dest.Component].CompareTo(currentCalculatedDist) > 0) 
                    {
                        ComponentWeights[dest.Key.dest.Component] = currentCalculatedDist;
                        parent[dest.Key.dest.Component] = src;
                        indexNDistance.Enqueue(dest.Key.dest.Component, currentCalculatedDist);
                    }
                }
            }
            return ComponentWeights;
        }
    }
}
