using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Extensions;

namespace Lib.Graphs.v2
{
 
    // A class to represent a connected, directed and weighted
    // graph
    public class Graph {
        // A class to represent a weighted edge in graph
        class Edge {
            public int from, to;
            public float weight;
            public Edge() { from = to; weight = 0f; }
        };
    
        int V, E;
        Edge[] edge;
    
        // Creates a graph with V vertices and E edges
        Graph(int v, int e)
        {
            V = v;
            E = e;
            edge = new Edge[e];
            for (int i = 0; i < e; ++i)
                edge[i] = new Edge();
        }
    
        // The main function that finds shortest distances from
        // src to all other vertices using Bellman-Ford
        // algorithm. The function also detects negative weight
        // cycle
        float[] BellmanFord(Graph graph, int src)
        {
            int V = graph.V, E = graph.E;
            float[] dist = new float[V];
    
            // Step 1: Initialize distances from src to all
            // other vertices as INFINITE
            for (int i = 0; i < V; ++i)
                dist[i] = int.MaxValue;
            dist[src] = 0;
    
            // Step 2: Relax all edges |V| - 1 times. A simple
            // shortest path from src to any other vertex can
            // have at-most |V| - 1 edges
            
            
            for (int s = 1; s < V; ++s) {
                for (int j = 0; j < E; ++j) {
                    int from = graph.edge[j].from;
                    int to = graph.edge[j].to;
                    float weight = graph.edge[j].weight;
                    if (dist[from] != int.MaxValue)
                    {
                        if(dist[from] + weight < dist[to])
                            dist[to] = dist[from] + weight;
                    }
                    /*
                    if (dist[u] != int.MaxValue
                    && dist[u] + weight < dist[v])
                    dist[v] = dist[u] + weight;*/
                    //if (dist[s-1,v] != int.MaxValue) //if is reachable
                    //    dist[s,v] = Math.Min(dist[s-1,v], dist[s-1,w] + weight);
                }
                
            }
                
            return dist;
        }
    
        // A utility function used to print the solution
        void printArr(int[] dist, int V)
        {
            Console.WriteLine("Vertex Distance from Source");
            for (int i = 0; i < V; ++i)
                Console.WriteLine(i + "\t\t" + dist[i]);
        }
        public static float[] manageFord(string[] lines) 
        {
            string[] line1 = lines[0].Split(' ');
            var V = int.Parse(line1[0]);
            var E = int.Parse(line1[1]);
            Graph graph = new Graph(V, E);
            for (int i = 1; i <= lines.Length - 2; i++) {
                string[] all_edge = lines[i].Split(' ');
                int u = int.Parse(all_edge[0])-1;
                int v = int.Parse(all_edge[1])-1;
                float w = float.Parse(all_edge[2]);
                graph.edge[i-1].from = u;
                graph.edge[i-1].to = v;
                graph.edge[i-1].weight = w;
            }

            int source = int.Parse(lines[lines.Length-1]) - 1;
            return graph.BellmanFord(graph, source);
        }
        
    }
}