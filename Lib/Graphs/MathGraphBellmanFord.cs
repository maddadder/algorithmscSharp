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
        
        public Tuple<Dictionary<T, float>,Dictionary<T, Tuple<T,float>>> BellmanFord(T src)
        {
            Dictionary<T, float>[] dist = new Dictionary<T, float>[Vertices.Count+1];
            Dictionary<T, Tuple<T,float>>[] predecessor = new Dictionary<T, Tuple<T,float>>[Vertices.Count+1];
            for(var i = 0;i<Vertices.Count+1;i++)
            {
                dist[i] = new Dictionary<T, float>();
                predecessor[i] = new Dictionary<T, Tuple<T,float>>();
            }
            
            
            foreach(var left in Vertices)
            {
                dist[0][left.Key] = float.MaxValue;
                predecessor[0][left.Key] = default;
            }
            dist[0][src] = 0;
            for(var i = 1;i<=Vertices.Count;i++)
            {
                var stable = true;
                foreach (T left in Vertices.Keys)
                {
                    var min = dist[i-1][left];
                    Edge<T> _edge = default;
                    foreach (var edge in Vertices[left].InEdge)
                    {
                        var weight = dist[i-1][edge.Key.dest.Component] + edge.Key.EdgeWeight;
                        if(weight < min)
                        {
                            min = weight;
                            _edge = edge.Key;
                        }
                    }
                    if(min < dist[i-1][left]){
                        stable = false;
                        dist[i][left] = min;
                        predecessor[i][left] = new Tuple<T, float>(_edge.dest.Component, _edge.EdgeWeight);
                    }
                    else
                    {
                        dist[i][left] = dist[i-1][left];
                        predecessor[i][left] = predecessor[i-1][left];
                    }
                }
                if(stable){
                    return new Tuple<Dictionary<T, float>, Dictionary<T, Tuple<T,float>>>(dist[i],predecessor[i]);
                }
            }
            return null;
        }
        
        public static Tuple<Dictionary<int, float>,Dictionary<int, Tuple<int,float>>> manageBellmanFord(MathGraph<int> mst, string[] lines) 
        {
            string[] line1 = lines[0].Split(' ');
            for (int i = 1; i <= lines.Length - 2; i++) {
                string[] all_edge = lines[i].Split(' ');
                int u = int.Parse(all_edge[0]);
                int v = int.Parse(all_edge[1]);
                float w = float.Parse(all_edge[2]);
                mst.AddEdge(u,v,w);
            }

            int source = int.Parse(lines[lines.Length-1]);
            return mst.BellmanFord(source);
        }
        public static SortedDictionary<T, MathGraph<T>> LoadBellmanFordPathsFromGraph(MathGraph<T> graph) 
        {
            SortedDictionary<T, MathGraph<T>> graphs = new SortedDictionary<T, MathGraph<T>>();
            var range = graph.GetVertices().Keys.OrderBy(x => x);
            foreach(var u in range)
            {
                graphs[u] = new MathGraph<T>(true);
                graphs[u].AddVertex(u);
                var paths = graph.BellmanFord(u);
                if(paths == null)
                    return null;
                MathGraph<T>.LoadBellmanFordDistances(graphs[u], paths.Item2, paths.Item1);
            }
            return graphs;
        }
        public static void LoadBellmanFordDistances(MathGraph<T> graph, Dictionary<T, Tuple<T, float>> edges, Dictionary<T, float> distances) 
        {
            foreach(var edge in edges)
            {
                
                if(edge.Value == default)
                    continue;
                T u = edge.Value.Item1;
                T v = edge.Key;
                float w = edge.Value.Item2;
                float distance = distances[v];
                graph.AddEdge(u,v,w,distance);
            }
        }
        
    }
}
