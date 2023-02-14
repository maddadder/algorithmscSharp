using System.Diagnostics;
using Lib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Data
{
    [TestClass]
    public class TestLoading
    {
        

        [TestMethod]
        public void Test_Loading()
        {
            string movieSourceFile = "../../../../Data/ml-latest-small/movies.csv";
            var movies = MovieLens.ImportMoviesCsvData(movieSourceFile);

            string ratingSourceFile = "../../../../Data/ml-latest-small/ratings.csv";
            var ratings = MovieLens.ImportRatingsCsvData(ratingSourceFile);
            MathGraph<string> graph = new MathGraph<string>(false);
            var joined = from movie in movies
                         join rating in ratings on movie.MovieId equals rating.MovieId
                         select new {
                            Title = movie.Title,
                            UserId = rating.UserId,
                            Value = rating.Value
                         };
            Debug.WriteLine($"There are {joined.Count()} Rows");
            int count = 1;
            float maxWeight = joined.Max(x => x.Value) + 1;
            
            foreach(var value in joined)
            {
                graph.AddEdge(value.UserId.ToString(), value.Title, maxWeight - value.Value);
                count+=1;
            }
            
            
            var dijkstra = graph.Dijkstra("Toy Story 3 (2010)");
            var dijkstraDist = dijkstra.Item1;
            foreach(var t in dijkstraDist.OrderBy(x => x.Value).Take(100))
            {
                Debug.WriteLine($"{t.Key},{t.Value}");
            }
            Debug.WriteLine("");
            foreach(var t in dijkstraDist.OrderByDescending(x => x.Value).Take(100))
            {
                Debug.WriteLine($"{t.Key},{t.Value}");
            }
            Debug.WriteLine("");

            var movie1 = "Star Wars: Episode IV - A New Hope (1977)";
            Debug.WriteLine($"{movie1} Sorted Asc");
            graph.prims_mst(movie1);
            var test2 = graph.GetVertices();
            graph.printComponentWeights(movie1, 100, true);
            Debug.WriteLine($"{movie1} Sorted Desc");
            graph.printComponentWeights(movie1, 100, false);
            var components = graph.CountComponents();
            Debug.WriteLine($"Components:{components}");
        }
    }
}