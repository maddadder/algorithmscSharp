using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lib.Graphs
{
    public class Vertex<T>
    {
        public List<Vertex<T>> EdgeList;
        public float EdgeWeight;
        public T Component;

        public Vertex(T component=default)
        {
            EdgeList = new List<Vertex<T>>();
            EdgeWeight = 100000;
            Component = component;
        }
    }


    public class MathGraph<T> where T : IComparable<T>
    {
        private string GraphName;

        private SortedDictionary<T, Vertex<T>> Vertices;
        private SortedDictionary<T, T> parent;
        private SortedDictionary<T, float> ComponentWeights;
        private SortedDictionary<T, int> Components;
        private int edgeCount;

        public MathGraph(string graphName = "None")
        {
            Initialize(graphName);
        }

        private void Initialize(string graphName = "None")
        {
            GraphName = graphName;
            Vertices = new SortedDictionary<T, Vertex<T>>();
            ComponentWeights = new SortedDictionary<T, float>();
            Components = new SortedDictionary<T, int>();
            parent = new SortedDictionary<T, T>();
            edgeCount = 0;
        }
        public SortedDictionary<T, float> GetComponentWeights()
        {
            return ComponentWeights;
        }
        public int CountVertices()
        {
            return Vertices.Count;
        }

        public int CountEdges()
        {
            return edgeCount;
        }

        public int CountComponents()
        {
            return Components.Count;
        }
        public SortedDictionary<T, int> GetComponents()
        {
            return Components;
        }
        public T GetParent(T vertex)
        {
            return parent[vertex];
        }
        public float CountConnectedTo(T vertex)
        {
            if (!ContainsVertex(vertex))
            {
                string msg = $"Vertex '{vertex}' is not in the graph";
                throw new ArgumentException(msg);
            }

            T component = GetFinalComponentName(vertex);
            return Components[component];
        }

        public int CountAdjacent(T vertex)
        {
            if (!ContainsVertex(vertex))
            {
                string msg = $"Vertex '{vertex}' is not in the graph";
                throw new ArgumentException(msg);
            }

            return Vertices[vertex].EdgeList.Count();
        }

        public void AddVertex(T vertex)
        {
            if (ContainsVertex(vertex))
            {
                string msg = $"Vertex '{vertex}' is already in the graph";
                throw new ArgumentException(msg);
            }

            Vertices.Add(vertex, new Vertex<T>(vertex));
            Components.Add(vertex, 1);
            return;
        }

        public void AddEdge(T vertex1, T vertex2, float weight = 1, bool isUndirectedGraph = true)
        {
            AddEdge(new Vertex<T>(vertex1), new Vertex<T>(vertex2), weight, isUndirectedGraph);
        }

        public void AddEdge(Vertex<T> vertex1, Vertex<T> vertex2, float weight = 1, bool isUndirectedGraph = true)
        {
            if (!ContainsVertex(vertex1.Component))
            {
                AddVertex(vertex1.Component);
            }

            if (!ContainsVertex(vertex2.Component))
            {
                AddVertex(vertex2.Component);
            }
            vertex1.EdgeWeight = weight;
            vertex2.EdgeWeight = weight;
            Vertices[vertex1.Component].EdgeList.Add(vertex2);
            Vertices[vertex1.Component].EdgeWeight = weight;
            if(isUndirectedGraph){
                Vertices[vertex2.Component].EdgeList.Add(vertex1);
                Vertices[vertex2.Component].EdgeWeight = weight;
            }
            edgeCount++;

            // Union Find algorithm to maintain graph components with each new edge
            T v1 = GetFinalComponentName(vertex1.Component);
            T v2 = GetFinalComponentName(vertex2.Component);
            if (!Equal(v1, v2))
            {
                if (Components[v1] < Components[v2])
                {
                    Vertices[v1].Component = v2;
                    Components[v2] += Components[v1];
                    Components.Remove(v1);
                }
                else
                {
                    Vertices[v2].Component = v1;
                    Components[v1] += Components[v2];
                    Components.Remove(v2);
                }
            }
        }

        private T GetFinalComponentName(T vertex)
        {
            T component = vertex;
            while (!Equal(component, Vertices[component].Component))
            {
                component = Vertices[component].Component;
            }
            return component;
        }

        public bool TestConnectedTo(T vertex1, T vertex2)
        {
            if (!ContainsVertex(vertex1))
            {
                string msg = $"Vertex '{vertex1}' is not in the graph";
                throw new ArgumentException(msg);
            }

            if (!ContainsVertex(vertex2))
            {
                string msg = $"Vertex '{vertex2}' is not in the graph";
                throw new ArgumentException(msg);
            }

            T component1 = GetFinalComponentName(vertex1);
            T component2 = GetFinalComponentName(vertex2);
            return (Equal(component1, component2));
        }

        public bool ContainsVertex(T vertex)
        {
            return Vertices.ContainsKey(vertex);
        }

        public bool ContainsEdge(Vertex<T> vertex1, Vertex<T> vertex2)
        {
            if (!ContainsVertex(vertex1.Component))
            {
                string msg = $"Vertex '{vertex1}' is not in the graph";
                throw new ArgumentException(msg);
            }

            if (!ContainsVertex(vertex2.Component))
            {
                string msg = $"Vertex '{vertex2}' is not in the graph";
                throw new ArgumentException(msg);
            }

            return Vertices[vertex1.Component].EdgeList.Contains(vertex2);
        }

        public List<T> FindFirstPath(T vertex1, T vertex2)
        {
            if (!ContainsVertex(vertex1))
            {
                string msg = $"Vertex '{vertex1}' is not in the graph";
                throw new ArgumentException(msg);
            }

            if (!ContainsVertex(vertex2))
            {
                string msg = $"Vertex '{vertex2}' is not in the graph";
                throw new ArgumentException(msg);
            }

            List<T> firstPath = new List<T>();
            SortedDictionary<T, bool> marked = ClearAllVertexMarks();
            SortedDictionary<T, T> edgeTo = new SortedDictionary<T, T>();
            DepthFirstPathTo(vertex1, vertex2, marked, edgeTo);

            if (!marked[vertex1])
            {
                string msg = $"Graph does not contain a path from '{vertex1}' to '{vertex2}'";
                throw new ArgumentException(msg);
            }

            firstPath.Add(vertex1);
            T curr = vertex1;
            while (!Equal(curr, vertex2))
            {
                curr = edgeTo[curr];
                firstPath.Add(curr);
            }

            return firstPath;
        }

        private void DepthFirstPathTo(T srcVertex,
                                      T dstVertex,
                                      SortedDictionary<T, bool> marked,
                                      SortedDictionary<T, T> edgeTo)
        {
            marked[dstVertex] = true;

            // Enumerate through all of the vertices that are adjacent to this one
            // If we have already visited the adjacent vertex, ignore it
            // Otherwise, we record it's position and then recurse deeper to it

            foreach (var adj in Vertices[dstVertex].EdgeList)
            {
                if (marked[adj.Component])
                {
                    continue;
                }

                edgeTo[adj.Component] = dstVertex;
                DepthFirstPathTo(srcVertex, adj.Component, marked, edgeTo);
            }
        }

        public List<T> FindShortestPath(T vertex1, T vertex2)
        {
            if (!ContainsVertex(vertex1))
            {
                string msg = $"Vertex '{vertex1}' is not in the graph";
                throw new ArgumentException(msg);
            }

            if (!ContainsVertex(vertex2))
            {
                string msg = $"Vertex '{vertex2}' is not in the graph";
                throw new ArgumentException(msg);
            }

            List<T> shortestPath = new List<T>();
            SortedDictionary<T, bool> marked = ClearAllVertexMarks();
            SortedDictionary<T, T> edgeTo = new SortedDictionary<T, T>();
            BreadthFirstPathTo(vertex1, vertex2, marked, edgeTo);

            if (!marked[vertex1])
            {
                string msg = $"Graph does not contain a path from '{vertex1}' to '{vertex2}'";
                throw new ArgumentException(msg);
            }

            T curr = vertex1;
            shortestPath.Add(vertex1);
            while (!Equal(curr, vertex2))
            {
                curr = edgeTo[curr];
                shortestPath.Add(curr);
            }

            return shortestPath;
        }

        private void BreadthFirstPathTo(T srcVertex,
                                        T dstVertex,
                                        SortedDictionary<T, bool> marked,
                                        SortedDictionary<T, T> edgeTo)
        {
            Queue<T> searchList = new Queue<T>();
            searchList.Enqueue(dstVertex);
            marked[dstVertex] = true;
            int count = 0;

            while (searchList.Count > 0)
            {
                T v = searchList.Dequeue();
                foreach (var adj in Vertices[v].EdgeList)
                {
                    if (marked[adj.Component])
                    {
                        continue;
                    }

                    marked[adj.Component] = true;
                    searchList.Enqueue(adj.Component);
                    edgeTo[adj.Component] = v;

                    if (Equal(srcVertex, adj.Component))
                    {
                        Console.WriteLine($"Search completed in {count} steps");
                        return;
                    }
                    count++;
                }
                count++;
            }
        }

        private SortedDictionary<T, bool> ClearAllVertexMarks()
        {
            SortedDictionary<T, bool> marks = new SortedDictionary<T, bool>();
            foreach (T key in Vertices.Keys)
            {
                marks[key] = false;
            }
            return marks;
        }

        private SortedDictionary<T, float> SetAllVertexDistances()
        {
            SortedDictionary<T, float> marks = new SortedDictionary<T, float>();
            foreach (T key in Vertices.Keys)
            {
                marks[key] = 100000;
            }
            return marks;
        }
        private SortedDictionary<T, T> SetAllVertexParents()
        {
            SortedDictionary<T, T> marks = new SortedDictionary<T, T>();
            foreach (T key in Vertices.Keys)
            {
                marks[key] = default;
            }
            return marks;
        }

        private bool Equal(T vertex1, T vertex2)
        {
            return (vertex1.CompareTo(vertex2) == 0);
        }

        public IEnumerable<T> EnumVertices()
        {
            foreach (T vertex in Vertices.Keys)
            {
                yield return vertex;
            }
        }

        public IEnumerable<T> EnumAdjacent(T vertex)
        {
            foreach (var edge in Vertices[vertex].EdgeList)
            {
                yield return edge.Component;
            }
        }

        public IEnumerable<T> EnumConnectedTo(T vertex)
        {
            T component = GetFinalComponentName(vertex);

            foreach (T potentialVertex in Vertices.Keys)
            {
                T potentialComponent = GetFinalComponentName(potentialVertex);
                if (Equal(component, potentialComponent))
                {
                    yield return potentialVertex;
                }
            }
        }

        public SortedDictionary<T, float> Dijkstra(T left)
        {
            ComponentWeights = SetAllVertexDistances();
            parent = SetAllVertexParents();
            SortedDictionary<T, bool> isVertexVisited = ClearAllVertexMarks();
            ComponentWeights[left] = 0;
            PriorityQueue<T,float> indexNDistance = new PriorityQueue<T,float>();
            indexNDistance.Enqueue(left, 0);
            while (indexNDistance.Count > 0) {
                left = indexNDistance.Dequeue();
                isVertexVisited[left] = true;
                foreach (var right in Vertices[left].EdgeList) {
                    var currentCalculatedDist = ComponentWeights[left] + right.EdgeWeight;
                    if (isVertexVisited[right.Component] == false && 
                        ComponentWeights[right.Component].CompareTo(currentCalculatedDist) > 0) 
                    {
                        ComponentWeights[right.Component] = currentCalculatedDist;
                        parent[right.Component] = left;
                        indexNDistance.Enqueue(right.Component, currentCalculatedDist);
                    }
                }
            }
            return ComponentWeights;
        }
        public System.Collections.Generic.SortedDictionary<T, Lib.Graphs.Vertex<T>> prims_mst(T left) {
            ComponentWeights = SetAllVertexDistances();
            parent = SetAllVertexParents();
            SortedDictionary<T, bool> isVertexVisited = ClearAllVertexMarks();
            ComponentWeights[left] = 0;
            PriorityQueue<T,float> indexNDistance = new PriorityQueue<T,float>();
            indexNDistance.Enqueue(left, 0);
            while (indexNDistance.Count > 0) {
                left = indexNDistance.Dequeue();
                isVertexVisited[left] = true;
                foreach (var right in Vertices[left].EdgeList) {
                    if (isVertexVisited[right.Component] == false && 
                        ComponentWeights[right.Component].CompareTo(right.EdgeWeight) > 0) 
                    {
                        ComponentWeights[right.Component] = right.EdgeWeight;
                        parent[right.Component] = left;
                        indexNDistance.Enqueue(right.Component, right.EdgeWeight);
                    }
                }
            }
            return Vertices;
        }
  
        public float print_distances(T source, int limit = 1000, bool? asc = true) {
            float total = 0;
            IEnumerable<T> results = null;
            if(asc == true){
                results = Vertices.Keys.OrderBy(x => ComponentWeights[x]).Take(limit);
            }
            else if(asc == false)
            {
                results = Vertices.Keys.OrderByDescending(x => ComponentWeights[x]).Take(limit);
            }
            else
            {
                results = Vertices.Keys.Take(limit);
            }
            foreach (T key in results)
            {
                var result = key.CompareTo(source);
                if (result != 0) {
                    total += ComponentWeights[key];
                    Debug.WriteLine("( {0} - {1} ) = {2}", parent[key], key, ComponentWeights[key]);
                }
            }
            Debug.WriteLine(total);
            return total;
        }
        
        public List<T> FindAccessibleVertices(T vertex)
        {
            List<T> accessibleVertices = new List<T>();
            Queue<T> verticesToVisit = new Queue<T>();

            verticesToVisit.Enqueue(vertex);

            while (verticesToVisit.Count != 0)
            {
                T currentVertexIndex = verticesToVisit.Dequeue();

                if (accessibleVertices.Contains(currentVertexIndex) == false)
                {
                    accessibleVertices.Add(currentVertexIndex);

                    var edges = Vertices[currentVertexIndex].EdgeList;

                    edges.ForEach(v => verticesToVisit.Enqueue(v.Component));
                }
            }

            return accessibleVertices;
        }

        public void DumpGraph()
        {
            if (GraphName != "None")
            {
                Console.WriteLine($"Graph: {GraphName}");
            }
            else
            {
                Console.WriteLine($"Unnamed Graph:");
            }

            foreach (T vertex in Vertices.Keys)
            {
                Console.Write($"{vertex}: ");
                foreach (var adj in Vertices[vertex].EdgeList)
                {
                    Console.Write($"{{{adj.Component}}} ");
                }
                Console.WriteLine();
            }
        }
        
        public static void renderGraph(SortedDictionary<int, Lib.Graphs.Vertex<int>> graph) 
        {
            var len = graph.Max(x => x.Key) + 2;
            Console.WriteLine("");
            for(var i = 0;i<len;i++)
            {
                if(graph.ContainsKey(i))
                {
                    for(var j = 0;j<len;j++)
                    {
                        if(graph[i].EdgeList.Select(_ => _.Component).Contains(j))
                        {
                            Console.Write($"⬜");
                        }
                        else{
                            Console.Write($"⬛");
                        }
                    }
                    Console.WriteLine("");
                }
                else
                {
                    for(var j = 0;j<len;j++)
                    {
                        Console.Write($"⬛");
                    }
                    Console.WriteLine("");
                }
            }
        }
        public override string ToString()
        {
            return $"Graph {GraphName}: {Vertices.Count} vertices and {edgeCount} edges";
        }
        public static SortedDictionary<int, Lib.Graphs.Vertex<int>> managePrimsMST(MathGraph<int> mst, string[] lines, bool isUndirectedGraph = true) 
        {
            string[] line1 = lines[0].Split(' ');
            for (int i = 1; i <= lines.Length - 2; i++) {
                string[] all_edge = lines[i].Split(' ');
                int u = int.Parse(all_edge[0]);
                int v = int.Parse(all_edge[1]);
                float w = float.Parse(all_edge[2]);
                mst.AddEdge(u,v,w,isUndirectedGraph);
            }

            int source = int.Parse(lines[lines.Length-1]);
            var result = mst.prims_mst(source);
            mst.print_distances(source);
            return result;
        }
    }
}
