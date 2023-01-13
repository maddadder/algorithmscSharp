using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lib.Graphs
{
    public class PrimsMST 
    {

        public struct Node {
            public int v, w;

            public Node(int v, int w) {
                this.v = v;
                this.w = w;
            }
        };
        
        public int nodes, edges;
        public List <Node>[] graph;
        int[] dis, parent;
        bool[] vis;

        public void initPrimsMST(int nodes, int edges) {
            this.nodes = nodes;
            this.edges = edges;

            dis = new int[nodes];
            parent = new int[nodes];
            vis = new bool[nodes];
            graph = new List<Node>[nodes];

            for (int i = 0; i < nodes; i++) {
                graph[i] = new List<Node>();
            }
        }
        public void AddNode(int u, int v, int w)
        {
            graph[u].Add(new Node(v, w));
            graph[v].Add(new Node(u, w));
        }

        public int minDistance() {
            int minVal = int.MaxValue, minIdx = -1;
            for (int i = 0; i < nodes; i++) {
                if (vis[i] == false && dis[i] < minVal) {
                    minVal = dis[i];
                    minIdx = i;
                }
            }
            return minIdx;
        }

        public void prims_mst(int source) {
            for (int i = 0; i < nodes; i++) {
                dis[i] = int.MaxValue;
                parent[i] = -1;
                vis[i] = false;
            }

            dis[source] = 0;
            vis[source] = true;

            PriorityQueue<int,int> pq = new PriorityQueue<int,int>();
            pq.Enqueue(source, 0);

            while (pq.Count > 0) {
                int u = pq.Dequeue();
                vis[u] = true;

                for (int j = 0; j < graph[u].Count; j++) {
                    int v = graph[u][j].v;
                    int w = graph[u][j].w;

                    if (vis[v] == false && dis[v] > w) {
                        dis[v] =  w;
                        parent[v] = u;
                        pq.Enqueue(v, dis[v]);
                    }
                }
            }
        }

        public int print_distance(int source) {
            var total = 0;
            for (int i = 0; i < nodes; i++) {
                if (i != source) {
                    total += dis[i];
                    Debug.WriteLine("( {0} - {1} ) = {2}", parent[i], i, dis[i]);
                }
            }
            Debug.WriteLine(total);
            return total;
        }

        public int managePrimsMST(string[] lines) {
            string[] line1 = lines[0].Split(' ');
            int nodes = int.Parse(line1[0]);
            int edges = int.Parse(line1[1]);

            initPrimsMST(nodes, edges);

            for (int i = 1; i <= edges; i++) {
                string[] all_edge = lines[i].Split(' ');
                int u = int.Parse(all_edge[0])-1;
                int v = int.Parse(all_edge[1])-1;
                int w = int.Parse(all_edge[2]);
                AddNode(u,v,w);
            }

            int source = int.Parse(lines[edges+1]);
            prims_mst(source);
            return print_distance(source);
        }
    }
}