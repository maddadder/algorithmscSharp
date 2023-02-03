using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Extensions;

namespace Lib.Graphs.v2
{
    public class Edge<T>{
        public Vertex<T> src;
        public Vertex<T> dest;
        public float EdgeWeight = 100000;
    }
    public class Vertex<T>
    {
        public List<Edge<T>> InEdges;
        public List<Edge<T>> OutEdges;
        
        public T Component;

        public Vertex(T component=default)
        {
            InEdges = new List<Edge<T>>();
            OutEdges = new List<Edge<T>>();
            Component = component;
        }
    }


    public class MathGraph<T> where T : IComparable<T>
    {
        private string GraphName;

        private SortedDictionary<T, Vertex<T>> Vertices;

        public MathGraph(string graphName = "None")
        {
            Initialize(graphName);
        }

        private void Initialize(string graphName = "None")
        {
            GraphName = graphName;
            Vertices = new SortedDictionary<T, Vertex<T>>();
        }
        
        public bool ContainsVertex(T vertex)
        {
            return Vertices.ContainsKey(vertex);
        }

        public void AddVertex(T vertex)
        {
            if (ContainsVertex(vertex))
            {
                string msg = $"Vertex '{vertex}' is already in the graph";
                throw new ArgumentException(msg);
            }

            Vertices.Add(vertex, new Vertex<T>(vertex));
            return;
        }

        public void AddEdge(T vertex1, T vertex2, float weight = 1)
        {
            AddEdge(new Vertex<T>(vertex1), new Vertex<T>(vertex2), weight);
        }

        public void AddEdge(Vertex<T> vertex1, Vertex<T> vertex2, float weight = 1)
        {
            if (!ContainsVertex(vertex1.Component))
            {
                AddVertex(vertex1.Component);
            }

            if (!ContainsVertex(vertex2.Component))
            {
                AddVertex(vertex2.Component);
            }
            
            var lEdge = new Edge<T>();
            lEdge.src = vertex1;
            lEdge.dest = vertex2;
            lEdge.EdgeWeight = weight;
            Vertices[vertex1.Component].OutEdges.Add(lEdge);
            Vertices[vertex2.Component].InEdges.Add(lEdge);
        }


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
                    foreach (var edge in Vertices[left].InEdges){
                        min = Math.Min(min, dist[i-1][edge.src.Component] + edge.EdgeWeight);
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
                int u = int.Parse(all_edge[0]) - 1;
                int v = int.Parse(all_edge[1]) -1 ;
                float w = float.Parse(all_edge[2]);
                mst.AddEdge(u,v,w);
            }

            int source = int.Parse(lines[lines.Length-1]) - 1;
            return mst.BellmanFord(source);
        }
        
    }
}
