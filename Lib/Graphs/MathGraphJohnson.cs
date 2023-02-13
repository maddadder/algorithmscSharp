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
        public static void LoadBellmanFordWeights(MathGraph<T> graph, Dictionary<Tuple<T, T>, float> edgeList, Dictionary<T, float> distances) 
        {
            foreach(var edge in edgeList)
            {
                T u = edge.Key.Item1;
                T v = edge.Key.Item2;
                float hu = distances[u];
                float hv = distances[v];
                float w = edge.Value;
                float distance = w + hu - hv;
                graph.AddEdge(u,v,distance);
            }
        }
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
            var distances = bf.Item1;
            var predecessor = bf.Item2;
            MathGraph<T> modifiedWeightsGraph = new MathGraph<T>(true);
            MathGraph<T>.LoadBellmanFordWeights(modifiedWeightsGraph, graphClone.EdgeList, distances);
            
            modifiedWeightsGraph.RemoveVertex(temp);
            //graphs[default] = bfGraphClone;
            
            foreach(var u in modifiedWeightsGraph.GetVertices().Keys.OrderBy(x => x))
            {
                graphs[u] = new MathGraph<T>(true);
                graphs[u].AddVertex(u);
                var dijkstraWeights = modifiedWeightsGraph.Dijkstra(u);
                foreach(var edge in modifiedWeightsGraph.EdgeList)
                {
                    var distance = dijkstraWeights[edge.Key.Item2];
                    var originalWeight = edge.Value;
                    var hu = distances[edge.Key.Item1];
                    var hv = distances[edge.Key.Item2];

                    // rolling sum
                    var sum = distance + hv;

                    //reconstruct original weight
                    var weight = originalWeight - hu + hv;

                    if(sum != float.MaxValue)
                    {
                        graphs[u].AddEdge(edge.Key.Item1, edge.Key.Item2, weight, sum);
                    }
                }
            }

            return graphs;
        }
        
    }
}
