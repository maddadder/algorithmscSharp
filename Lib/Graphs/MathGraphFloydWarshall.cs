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
        public Tuple<Dictionary<T,Dictionary<T,T>>,Dictionary<T,Dictionary<T,float>>> FloydWarshal()
        {
            var dist = new Dictionary<T, Dictionary<T, Dictionary<T, float>>>();
            var next = new Dictionary<T, Dictionary<T, T>>();
            var first = Vertices.First();
            dist[first.Key] = new Dictionary<T, Dictionary<T, float>>();
            foreach (var v in Vertices.Keys)
            {
                dist[first.Key][v] = new Dictionary<T, float>();
                next[v] = new Dictionary<T, T>();
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
            return mst.FloydWarshal();
        }
        public static List<T> FloydWarshalPath(T u, T v, Dictionary<T,Dictionary<T,T>> next){
            var path = new List<T>();
            if(next[u][v] == null)
                return path;
            path.Add(u);
            while(u.CompareTo(v) != 0){
                u = next[u][v];
                path.Add(u);
            }
            return path;
        }
    }
}
