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
        public SortedDictionary<T,SortedDictionary<T,float>> FloydWarshal()
        {
            SortedDictionary<T,SortedDictionary<T,SortedDictionary<T,float>>> dist = 
                new SortedDictionary<T, SortedDictionary<T, SortedDictionary<T, float>>>();
            var first = Vertices.First();
            dist[first.Key] = new SortedDictionary<T, SortedDictionary<T, float>>();
            foreach (T v in Vertices.Keys)
            {
                dist[first.Key][v] = new SortedDictionary<T, float>();
                foreach (T w in Vertices.Keys)
                {
                    var edge = new Tuple<T,T>(v,w);
                    if(v.CompareTo(w) == 0){
                        dist[first.Key][v][w] = 0;
                    }
                    else if(EdgeList.ContainsKey(edge))
                    {
                        dist[first.Key][v][w] = EdgeList[edge];
                    }
                    else
                    {
                        dist[first.Key][v][w] = float.MaxValue;
                    }
                }
            }
            var previousK = first.Key;
            foreach (T k in Vertices.Keys)
            {
                if(k.CompareTo(first.Key) == 0)
                {
                    previousK = k;
                    continue;
                }
                dist[k] = new SortedDictionary<T, SortedDictionary<T, float>>();
                foreach (T v in Vertices.Keys)
                {
                    dist[k][v] = new SortedDictionary<T, float>();
                    foreach (T w in Vertices.Keys)
                    {
                        dist[k][v][w] = Math.Min(dist[previousK][v][w], 
                            dist[previousK][v][k] + dist[previousK][k][w]);
                    }
                }
                previousK = k;
            }
            foreach (T v in Vertices.Keys)
            {
                if(dist[Vertices.Keys.Last()][v][v] < 0)
                {
                    //contains negative cycle
                    return null;
                }
            }
            return dist[Vertices.Keys.Last()];
        }
        
        public static SortedDictionary<int,SortedDictionary<int,float>> manageFloydWarshal(MathGraph<int> mst, string[] lines) 
        {
            string[] line1 = lines[0].Split(' ');
            for (int i = 1; i <= lines.Length - 2; i++) {
                string[] all_edge = lines[i].Split(' ');
                int u = int.Parse(all_edge[0]);
                int v = int.Parse(all_edge[1]);
                float w = float.Parse(all_edge[2]);
                mst.AddEdge(u,v,w, isUndirectedGraph:false);
            }

            int source = int.Parse(lines[lines.Length-1]);
            return mst.FloydWarshal();
        }
        
        
    }
}
