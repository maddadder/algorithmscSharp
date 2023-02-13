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
        
        public static SortedDictionary<T, MathGraph<T>> LoadJohnsonPathsFromGraph(MathGraph<T> graph) 
        {
            SortedDictionary<T, MathGraph<T>> graphs = new SortedDictionary<T, MathGraph<T>>();
            T temp = default;
            var graphClone = new MathGraph<T>(true);
            foreach(var edge in graph.EdgeList){
                graphClone.AddEdge(edge.Key.Item1, edge.Key.Item2, edge.Value);
            }
            foreach(var v in graphClone.GetVertices().Keys.ToList())
            {
                graphClone.AddEdge(temp, v, 0);
            }
            var bf = graphClone.BellmanFord(temp);
            MathGraph<T> bfGraphClone = new MathGraph<T>(true);
            MathGraph<T>.LoadBellmanFordWeights(bfGraphClone, graphClone.EdgeList, bf.Item1);
            
            bfGraphClone.RemoveVertex(temp);
            //graphs[default] = bfGraphClone;
            
            foreach(var u in bfGraphClone.GetVertices().Keys)
            {
                graphs[u] = new MathGraph<T>(true);
                graphs[u].AddVertex(u);
                var weights = bfGraphClone.Dijkstra(u);
                foreach(var edge in bfGraphClone.EdgeList.Zip(graph.EdgeList))
                {
                    //var weight1 = bfGraphClone.ComponentWeights[edge.First.Key.Item1];
                    var weight2 = bfGraphClone.ComponentWeights[edge.First.Key.Item2];
                    var originalWeight = edge.Second.Value;
                    //var distance = originalWeight + weight1 - weight2;
                    graphs[u].AddEdge(edge.First.Key.Item1, edge.First.Key.Item2, originalWeight, weight2);
                }
            }

            return graphs;
        }
        
    }
}
