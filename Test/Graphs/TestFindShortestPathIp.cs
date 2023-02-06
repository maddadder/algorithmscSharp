using Lib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace Test
{
    [TestClass]
    public class TestFindShortestPathIp
    {
        [TestMethod]
        public void Test_FindShortestPathIp()
        {
            MathGraph<string> graph = new MathGraph<string>();
            List<Tuple<IPAddress,IPAddress>> googleRoute = new List<Tuple<IPAddress, IPAddress>>();
            List<Tuple<IPAddress,IPAddress>> bingRoute = new List<Tuple<IPAddress, IPAddress>>();
            List<Tuple<IPAddress,IPAddress>> ntpRoute = new List<Tuple<IPAddress, IPAddress>>();
            string me = "192.168.4.1";
            string google = "172.217.14.206";
            string bing = "13.107.21.200";
            string ntp = "65.100.46.166";
            TraceRoute.GetTraceRoute(google,1, googleRoute);
            TraceRoute.GetTraceRoute(bing,1, bingRoute);
            TraceRoute.GetTraceRoute(ntp,1, ntpRoute);
            var i = 0;
            foreach (var entry in googleRoute
            .Concat(bingRoute)
            .Concat(ntpRoute)
            .Where(x => x.Item1 != null && x.Item2 != null))
            {
                var previous = entry.Item1.ToString();
                var next = entry.Item2.ToString();

                if(!graph.ContainsVertex(previous))
                {
                    graph.AddVertex(previous);
                }

                if(!graph.ContainsVertex(next))
                {
                    graph.AddVertex(next);
                }
                graph.AddEdge(previous, next);
                i++;
            }
            Debug.WriteLine("me => google:");
            GetShortestPath(graph, me, google);
            Debug.WriteLine("me => bing:");
            GetShortestPath(graph, me, bing);
            Debug.WriteLine("bing => google:");
            GetShortestPath(graph, bing, google);
            Debug.WriteLine("me => ntp:");
            GetShortestPath(graph, me, ntp);
            Debug.WriteLine("google => ntp:");
            GetShortestPath(graph, google, ntp);
        }
        public static void GetShortestPath(MathGraph<string> graph, string nodeA, string nodeB)
        {
            var node1 = nodeA;
            var node2 = nodeB;

            if(!graph.ContainsVertex(node1)) // check actor 1 exists
            {
                Debug.WriteLine($"node '{node1}' not found.");
                return;
            }

            if (!graph.ContainsVertex(node2)) // check actor 2 exists
            {
                Debug.WriteLine($"node '{node2}' not found.");
                return;
            }

            //5. Find the shortest path from one actor/ actress to the other
            var results = graph.FindShortestPath(node1, node2);

            //6. Calculate the degrees of separation score
            int degree = (results.Count - 1) / 2;
            Debug.WriteLine($"{node1} has been in {graph.CountAdjacent(node1)} route(s) and {node2} has been in {graph.CountAdjacent(node2)} route(s).");
            Debug.WriteLine($"The degree of separation between {node1} and {node2} is {degree}.");
            Debug.WriteLine("SHORTEST PATH:");

            //7. Display the path from one person to the other
            for (int j = 0; j < results.Count - 2; j += 2)
            {
                Debug.WriteLine($"{results[j]} was in {results[j + 1]} with {results[j + 2]}.");
            }

            var source = node1;

            // Act
            graph.prims_mst(source);
            graph.printComponentWeights(source);

            var components = graph.CountComponents();
            Debug.WriteLine($"Components: {components}");
        }
    }
}
