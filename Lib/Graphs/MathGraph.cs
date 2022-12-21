using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Graphs
{
    public class Vertex<T>
    {
        public List<T> EdgeList;
        public T Component;

        public Vertex(T component=default)
        {
            EdgeList = new List<T>();
            Component = component;
        }
    }


    public class MathGraph<T> where T : IComparable<T>
    {
        private string GraphName;

        private SortedDictionary<T, Vertex<T>> Vertices;
        private SortedDictionary<T, int> ComponentWeights;
        private int edgeCount;

        public MathGraph(string graphName = "None")
        {
            Initialize(graphName);
        }

        private void Initialize(string graphName = "None")
        {
            GraphName = graphName;
            Vertices = new SortedDictionary<T, Vertex<T>>();
            ComponentWeights = new SortedDictionary<T, int>();
            edgeCount = 0;
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
            return ComponentWeights.Count;
        }

        public int CountConnectedTo(T vertex)
        {
            if (!ContainsVertex(vertex))
            {
                string msg = $"Vertex '{vertex}' is not in the graph";
                throw new ArgumentException(msg);
            }

            T component = GetFinalComponentName(vertex);
            return ComponentWeights[component];
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
            ComponentWeights.Add(vertex, 1);
            return;
        }

        public void AddEdge(T vertex1, T vertex2)
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

            Vertices[vertex1].EdgeList.Add(vertex2);
            Vertices[vertex2].EdgeList.Add(vertex1);
            edgeCount++;

            // Union Find algorithm to maintain graph components with each new edge
            T v1 = GetFinalComponentName(vertex1);
            T v2 = GetFinalComponentName(vertex2);
            if (!Equal(v1, v2))
            {
                if (ComponentWeights[v1] < ComponentWeights[v2])
                {
                    Vertices[v1].Component = v2;
                    ComponentWeights[v2] += ComponentWeights[v1];
                    ComponentWeights.Remove(v1);
                }
                else
                {
                    Vertices[v2].Component = v1;
                    ComponentWeights[v1] += ComponentWeights[v2];
                    ComponentWeights.Remove(v2);
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

        public bool ContainsEdge(T vertex1, T vertex2)
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

            return Vertices[vertex1].EdgeList.Contains(vertex2);
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

            foreach (T adj in Vertices[dstVertex].EdgeList)
            {
                if (marked[adj])
                {
                    continue;
                }

                edgeTo[adj] = dstVertex;
                DepthFirstPathTo(srcVertex, adj, marked, edgeTo);
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
                foreach (T adj in Vertices[v].EdgeList)
                {
                    if (marked[adj])
                    {
                        continue;
                    }

                    marked[adj] = true;
                    searchList.Enqueue(adj);
                    edgeTo[adj] = v;

                    if (Equal(srcVertex, adj))
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

        private SortedDictionary<T, int> SetAllVertexDistances()
        {
            SortedDictionary<T, int> marks = new SortedDictionary<T, int>();
            foreach (T key in Vertices.Keys)
            {
                marks[key] = 10000;
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
            foreach (T edge in Vertices[vertex].EdgeList)
            {
                yield return edge;
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

        public SortedDictionary<T, int> FindClosestDistancesUsingHeap(T sourceVertex)
        {
            List<T> accessibleVertices = FindAccessibleVertices(sourceVertex);

            SortedDictionary<T, int>  distances = SetAllVertexDistances();
            SortedDictionary<T, bool> isVertexVisited = ClearAllVertexMarks();

            distances[sourceVertex] = 0;

            int numOfVisitedVertices = 0;

            PriorityQueue<T,int> indexNDistance = new PriorityQueue<T,int>();
            indexNDistance.Enqueue(sourceVertex, 0);

            while (numOfVisitedVertices != accessibleVertices.Count)
            {
                // Remove one from priority queue
                var currentVisitedIndex = indexNDistance.Dequeue();

                // If it's not visited visit
                if (isVertexVisited[currentVisitedIndex] == false)
                {
                    isVertexVisited[currentVisitedIndex] = true;
                    numOfVisitedVertices++;

                    List<T> edgesFromVertex = Vertices[currentVisitedIndex].EdgeList;

                    foreach (var edge in edgesFromVertex)
                    {
                        var edgeWight = 1; //hardcoded for now
                        int currentCalculatedDist = edgeWight + distances[currentVisitedIndex];

                        if (distances[edge] > currentCalculatedDist)
                        {
                            distances[edge] = currentCalculatedDist;
                            indexNDistance.Enqueue(edge, currentCalculatedDist);
                        }
                    }
                }
            }

            return distances;
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

                    List<T> edges = Vertices[currentVertexIndex].EdgeList;

                    edges.ForEach(v => verticesToVisit.Enqueue(v));
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
                foreach (T adj in Vertices[vertex].EdgeList)
                {
                    Console.Write($"{{{adj}}} ");
                }
                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            return $"Graph {GraphName}: {Vertices.Count} vertices and {edgeCount} edges";
        }
    }
}
