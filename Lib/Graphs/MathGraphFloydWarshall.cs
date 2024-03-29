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
        public Tuple<Dictionary<T,Dictionary<T,T>>,Dictionary<T,Dictionary<T,float>>> FloydWarshall()
        {
            var dist = new Dictionary<T, Dictionary<T, Dictionary<T, float>>>();
            var next = new Dictionary<T, Dictionary<T, T>>();
            T first = default;
            dist[first] = new Dictionary<T, Dictionary<T, float>>();
            foreach (var v in Vertices.Keys)
            {
                dist[first][v] = new Dictionary<T, float>();
                next[v] = new Dictionary<T, T>();
                foreach (var w in Vertices.Keys)
                {
                    var edge = new Tuple<T,T>(v,w);
                    if(v.CompareTo(w) == 0){
                        dist[first][v][w] = 0;
                        next[v][w] = w;
                    }
                    else if(EdgeList.ContainsKey(edge))
                    {
                        dist[first][v][w] = EdgeList[edge];
                        next[v][w] = w;
                    }
                    else
                    {
                        dist[first][v][w] = float.MaxValue;
                    }
                }
            }
            var previousK = first;
            foreach (var k in Vertices.Keys)
            {
                dist[k] = new Dictionary<T, Dictionary<T, float>>();
                foreach (var v in Vertices.Keys)
                {
                    dist[k][v] = new Dictionary<T, float>();
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
            
            return new Tuple<Dictionary<T, Dictionary<T, T>>, 
                Dictionary<T, Dictionary<T, float>>>(next, dist[Vertices.Keys.Last()]);;
        }
        
        public static Tuple<Dictionary<int,Dictionary<int,int>>, Dictionary<int,Dictionary<int,float>>> manageFloydWarshal(MathGraph<int> mst, string[] lines) 
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
        public static LinkedList<T> FloydWarshalPath(T u, T v, Dictionary<T,Dictionary<T,T>> next){
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
        public static SortedDictionary<T, MathGraph<T>> LoadFloydWarshalPaths(MathGraph<T> graph, Dictionary<T,Dictionary<T,T>> next, Dictionary<T,Dictionary<T,float>> distances) 
        {
            SortedDictionary<T, MathGraph<T>> graphs = new SortedDictionary<T, MathGraph<T>>();
            foreach(var u in graph.GetVertices().Keys)
            {
                graphs[u] = new MathGraph<T>(true);
                foreach(var v in graph.GetVertices().Keys)
                {
                    if(u.CompareTo(v) != 0)
                    {
                        var list = FloydWarshalPath(u,v,next);
                        var sum = 0f;
                        for (var node = list.First; node != null; node = node.Next)
                        {
                            if(node.Next == null)
                                continue;
                            Tuple<T,T> edge = new Tuple<T, T>(node.Value,node.Next.Value);
                            float w = graph.EdgeList[edge];
                            var distance = distances[node.Value][node.Next.Value];
                            sum+=distance;
                            if(w == distance)
                            {
                                graphs[u].AddEdge(node.Value,node.Next.Value,w, sum);
                            }
                        }
                    }
                }
            }
            return graphs;
        }
        public static MathGraph<T> LoadFloydWarshalGraph(MathGraph<T> graph, Dictionary<T,Dictionary<T,T>> next, Dictionary<T,Dictionary<T,float>> distances) 
        {
            MathGraph<T> result = new MathGraph<T>(true);
            foreach(var u in graph.GetVertices().Keys)
            {
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
                                result.AddEdge(node.Value,node.Next.Value,w);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
