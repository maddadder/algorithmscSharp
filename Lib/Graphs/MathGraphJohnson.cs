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
        public Tuple<Dictionary<T,Dictionary<T,float>>, Dictionary<T,Dictionary<T,Tuple<T,float>>>> JohnsonAlgorithm()
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
            //contains negative cycle
            if(bf == null)
                return null;
            var distances = bf.Item1;
            var predecessor = bf.Item2;
            MathGraph<T> modifiedWeightsGraph = new MathGraph<T>(true);
            MathGraph<T>.LoadBellmanFordWeights(modifiedWeightsGraph, graphClone.EdgeList, distances);
            
            modifiedWeightsGraph.RemoveVertex(temp);
            //graphs[default] = bfGraphClone;
            Dictionary<T, Dictionary<T, float>> A = new Dictionary<T, Dictionary<T, float>>();
            var next = new Dictionary<T,Dictionary<T, Tuple<T, float>>>();
            foreach(var v in modifiedWeightsGraph.GetVertices().Keys)
            {
                var dijkstra = modifiedWeightsGraph.Dijkstra(v);
                A[v] = dijkstra.Item1;
                next[v] = dijkstra.Item2;
            }
            foreach(var u in modifiedWeightsGraph.GetVertices().Keys)
            {
                foreach(var v in modifiedWeightsGraph.GetVertices().Keys)
                {
                    A[u][v] = A[u][v] - distances[u] + distances[v];
                }
            }
            return new Tuple<Dictionary<T, Dictionary<T, float>>, Dictionary<T, Dictionary<T, Tuple<T, float>>>>(A,next);
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
            //contains negative cycle
            if(bf == null)
                return null;
            var bfDistances = bf.Item1;
            var predecessor = bf.Item2;
            MathGraph<T> modifiedWeightsGraph = new MathGraph<T>(true);
            MathGraph<T>.LoadBellmanFordWeights(modifiedWeightsGraph, graphClone.EdgeList, bfDistances);
            
            modifiedWeightsGraph.RemoveVertex(temp);
            //graphs[default] = bfGraphClone;
            
            foreach(var vertex in modifiedWeightsGraph.GetVertices().Keys.OrderBy(x => x))
            {
                graphs[vertex] = new MathGraph<T>(true);
                graphs[vertex].AddVertex(vertex);
                var dijkstra = modifiedWeightsGraph.Dijkstra(vertex);
                var dijkstraWeights = dijkstra.Item1;
                var dijkstraPrevious = dijkstra.Item2;
                foreach(var edge in dijkstraPrevious)
                {
                    if(edge.Value == null)
                        continue;
                    var dijkstrDistance = dijkstraWeights[edge.Key];
                    var modifiedWeight = edge.Value.Item2;
                    var u = edge.Value.Item1;
                    var v = edge.Key;
                    var hu = bfDistances[u];
                    var hv = bfDistances[v];

                    var rollingSum = dijkstrDistance + hv;

                    //reconstruct original weight
                    var originalWeight = modifiedWeight - hu + hv;

                    graphs[vertex].AddEdge(edge.Value.Item1, edge.Key, originalWeight, rollingSum);
                }
            }

            return graphs;
        }
        
    }
}
