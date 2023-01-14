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
            string movieSourceFile = "../../../ml-latest-small/movies.csv";
            var movies = MovieLens.ImportMoviesCsvData(movieSourceFile);

            string ratingSourceFile = "../../../ml-latest-small/ratings.csv";
            var ratings = MovieLens.ImportRatingsCsvData(ratingSourceFile);
            MathGraph<string> graph = new MathGraph<string>();
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
            
            
            var test = graph.FindClosestDistancesUsingHeap("Toy Story 3 (2010)");
            foreach(var t in test.OrderBy(x => x.Value).Take(100))
            {
                Debug.WriteLine($"{t.Key},{t.Value}");
            }
            
            var components = graph.CountComponents();
            Debug.WriteLine($"Components:{components}");
        }
    }
}