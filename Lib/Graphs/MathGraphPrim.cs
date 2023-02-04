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
        public SortedDictionary<T, float> prims_mst(T left) {
            ComponentWeights = SetAllVertexDistances();
            parent = SetAllVertexParents();
            SortedDictionary<T, bool> isVertexVisited = ClearAllVertexMarks();
            ComponentWeights[left] = 0;
            PriorityQueue<T,float> indexNDistance = new PriorityQueue<T,float>();
            indexNDistance.Enqueue(left, 0);
            while (indexNDistance.Count > 0) {
                left = indexNDistance.Dequeue();
                isVertexVisited[left] = true;
                foreach (var right in Vertices[left].InEdge) {
                    if (isVertexVisited[right.dest.Component] == false && 
                        ComponentWeights[right.dest.Component].CompareTo(right.EdgeWeight) > 0) 
                    {
                        ComponentWeights[right.dest.Component] = right.EdgeWeight;
                        parent[right.dest.Component] = left;
                        indexNDistance.Enqueue(right.dest.Component, right.EdgeWeight);
                    }
                }
                foreach (var right in Vertices[left].OutEdge) {
                    if (isVertexVisited[right.dest.Component] == false && 
                        ComponentWeights[right.dest.Component].CompareTo(right.EdgeWeight) > 0) 
                    {
                        ComponentWeights[right.dest.Component] = right.EdgeWeight;
                        parent[right.dest.Component] = left;
                        indexNDistance.Enqueue(right.dest.Component, right.EdgeWeight);
                    }
                }
            }
            return ComponentWeights;
        }
        public static SortedDictionary<int, Lib.Graphs.Vertex<int>> managePrimsMST(MathGraph<int> mst, string[] lines, bool isUndirectedGraph = true) 
        {
            string[] line1 = lines[0].Split(' ');
            for (int i = 1; i <= lines.Length - 2; i++) {
                string[] all_edge = lines[i].Split(' ');
                int u = int.Parse(all_edge[0]);
                int v = int.Parse(all_edge[1]);
                float w = float.Parse(all_edge[2]);
                mst.AddEdge(u,v,w,isUndirectedGraph);
            }

            int source = int.Parse(lines[lines.Length-1]);
            mst.prims_mst(source);
            var result = mst.GetVertices();
            mst.print_distances(source);
            return result;
        }
    }
}
