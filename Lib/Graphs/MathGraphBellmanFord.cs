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
        
        public SortedDictionary<T, float> BellmanFord(T src)
        {
            SortedDictionary<T, float>[] dist = new SortedDictionary<T, float>[Vertices.Count+1];
            for(var i = 0;i<Vertices.Count+1;i++)
            {
                dist[i] = new SortedDictionary<T, float>();
            }
            dist[0][src] = 0;
            foreach(var v in Vertices){
                if(v.Key.CompareTo(src) != 0){
                    dist[0][v.Key] = float.MaxValue;
                }
            }
            for(var i = 1;i<=Vertices.Count;i++)
            {
                var stable = true;
                foreach (T left in Vertices.Keys)
                {
                    var min = dist[i-1][left];
                    foreach (var edge in Vertices[left].InEdge){
                        min = Math.Min(min, dist[i-1][edge.Key.dest.Component] + edge.Key.EdgeWeight);
                    }
                    if(min < dist[i-1][left]){
                        stable = false;
                        dist[i][left] = min;
                    }
                    else
                    {
                        dist[i][left] = dist[i-1][left];
                    }
                }
                if(stable){
                    return dist[i];
                }
            }
            return null;
        }
        
        public static SortedDictionary<int, float> manageBellmanFord(MathGraph<int> mst, string[] lines) 
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
            return mst.BellmanFord(source);
        }
        
        
    }
}
