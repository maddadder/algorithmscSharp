using System;
using System.Collections.Generic;

namespace Lib.Graphs
{
    

    public partial class MathGraph<T> where T : IComparable<T>
    {
        public Tuple<Dictionary<T, float>,Dictionary<T, Tuple<T,float>>> Dijkstra(T src)
        {
            ComponentWeights = SetAllVertexDistances();
            parent = SetAllVertexParents();
            Dictionary<T, bool> isVertexVisited = ClearAllVertexMarks();
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
                        parent[dest.Key.dest.Component] = new Tuple<T, float>(src, dest.Key.EdgeWeight);
                        indexNDistance.Enqueue(dest.Key.dest.Component, currentCalculatedDist);
                    }
                }
            }
            return new Tuple<Dictionary<T, float>,Dictionary<T, Tuple<T,float>>>(ComponentWeights, parent);
        }
    }
}
