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
        public Tuple<SortedDictionary<T,SortedDictionary<T,T>>,SortedDictionary<T,SortedDictionary<T,float>>> FloydWarshall()
        {
            var dist = new SortedDictionary<T, SortedDictionary<T, SortedDictionary<T, float>>>();
            var next = new SortedDictionary<T, SortedDictionary<T, T>>();
            var first = Vertices.First();
            dist[first.Key] = new SortedDictionary<T, SortedDictionary<T, float>>();
            foreach (var v in Vertices.Keys)
            {
                dist[first.Key][v] = new SortedDictionary<T, float>();
                next[v] = new SortedDictionary<T, T>();
                foreach (var w in Vertices.Keys)
                {
                    var edge = new Tuple<T,T>(v,w);
                    if(v.CompareTo(w) == 0){
                        dist[first.Key][v][w] = 0;
                        next[v][w] = w;
                    }
                    else if(EdgeList.ContainsKey(edge))
                    {
                        dist[first.Key][v][w] = EdgeList[edge];
                        next[v][w] = w;
                    }
                    else
                    {
                        dist[first.Key][v][w] = float.MaxValue;
                    }
                }
            }
            var previousK = first.Key;
            foreach (var k in Vertices.Keys)
            {
                if(k.CompareTo(first.Key) == 0)
                {
                    previousK = k;
                    continue;
                }
                dist[k] = new SortedDictionary<T, SortedDictionary<T, float>>();
                foreach (var v in Vertices.Keys)
                {
                    dist[k][v] = new SortedDictionary<T, float>();
                    foreach (var w in Vertices.Keys)
                    {
                        if(dist[previousK][v][w] > dist[previousK][v][k] + dist[previousK][k][w])
                        {
                            dist[k][v][w] = dist[previousK][v][k] + dist[previousK][k][w];
                            next[v][w] = next[v][k];
                        }
                        else
                        {
                            dist[k][v][w] = dist[previousK][v][w];
                        }
                    }
                }
                previousK = k;
            }
            foreach (var v in Vertices.Keys)
            {
                if(dist[Vertices.Keys.Last()][v][v] < 0)
                {
                    //contains negative cycle
                    return null;
                }
            }
            
            return new Tuple<SortedDictionary<T, SortedDictionary<T, T>>, 
                SortedDictionary<T, SortedDictionary<T, float>>>(next, dist[Vertices.Keys.Last()]);;
        }
        
        public static Tuple<SortedDictionary<int,SortedDictionary<int,int>>, SortedDictionary<int,SortedDictionary<int,float>>> manageFloydWarshal(MathGraph<int> mst, string[] lines) 
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
            return mst.FloydWarshall();
        }
        public static LinkedList<T> FloydWarshalPath(T u, T v, SortedDictionary<T,SortedDictionary<T,T>> next){
            var path = new LinkedList<T>();
            if(!next[u].ContainsKey(v))
                return path;
            path.AddFirst(u);
            while(u.CompareTo(v) != 0){
                u = next[u][v];
                path.AddLast(u);
            }
            return path;
        }
        public static Dictionary<T, MathGraph<T>> LoadFloydWarshalPaths(MathGraph<T> graph, SortedDictionary<T,SortedDictionary<T,T>> next, SortedDictionary<T,SortedDictionary<T,float>> distances) 
        {
            Dictionary<T, MathGraph<T>> graphs = new Dictionary<T, MathGraph<T>>(next.Count);
            foreach(var u in graph.GetVertices().Keys)
            {
                graphs[u] = new MathGraph<T>(true);
                foreach(var v in graph.GetVertices().Keys)
                {
                    if(u.CompareTo(v) != 0)
                    {
                        var list = FloydWarshalPath(u,v,next);
                        for (var node = list.First; node != null; node = node.Next)
                        {
                            if(node.Next == null)
                                continue;
                            Tuple<T,T> edge = new Tuple<T, T>(node.Value,node.Next.Value);
                            float w = graph.EdgeList[edge];
                            var distance = distances[node.Value][node.Next.Value];
                            if(w == distance)
                            {
                                graphs[u].AddEdge(node.Value,node.Next.Value,w);
                            }
                        }
                    }
                }
            }
            return graphs;
        }
    }
}
