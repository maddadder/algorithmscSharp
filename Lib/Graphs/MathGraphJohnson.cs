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
        public Dictionary<T, Dictionary<T, float>> JohnsonAlgorithm()
        {
            SortedDictionary<T, MathGraph<T>> graphs = new SortedDictionary<T, MathGraph<T>>();
            T temp = default;
            var graphClone = new MathGraph<T>(true);
            foreach(var edge in this.EdgeList){
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
            Dictionary<T, Dictionary<T, float>> A = new Dictionary<T, Dictionary<T, float>>();
            foreach(var v in modifiedWeightsGraph.GetVertices().Keys)
            {
                A[v] = modifiedWeightsGraph.Dijkstra(v);
            }
            foreach(var u in modifiedWeightsGraph.GetVertices().Keys){
                foreach(var v in modifiedWeightsGraph.GetVertices().Keys)
                {
                    A[u][v] = A[u][v] - distances[u] + distances[v];
                }
            }
            return A;
        }
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
                    var minDistance = dijkstraWeights[edge.Key.Item2];
                    var modifiedWeight = edge.Value;
                    var hu = distances[edge.Key.Item1];
                    var hv = distances[edge.Key.Item2];
                    //A[u][v] - h[u] + h[v]
                    var rollingSum = minDistance - hu + hv;

                    //reconstruct original weight
                    var originalWeight = modifiedWeight - hu + hv;

                    if(rollingSum != float.MaxValue)
                    {
                        graphs[u].AddEdge(edge.Key.Item1, edge.Key.Item2, originalWeight, rollingSum);
                    }
                }
            }

            return graphs;
        }
        
    }
}
