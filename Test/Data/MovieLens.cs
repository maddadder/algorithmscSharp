
using System.Globalization;

namespace Test.Data
{
    public static class MovieLens
    {
        public static IEnumerable<Movie> ImportMoviesCsvData(string filePath)
        {
            // Read the csv file
            var rows = File.ReadAllLines(filePath).Select(x => x.Split(',')).Skip(1);

            // Declare a list for the movies
            var movies = new List<Movie>();

            // For each row...
            foreach (var line in rows)
            {
                // If the was a comma in the title...
                if (line.Count() != 3)
                {
                    // Concatenate the title parts
                    line[1] = line.Skip(1).Take(line.Length - 2).Aggregate((x, y) => x + "," + y);
                }

                // Add the new movie
                movies.Add(new Movie()
                {
                    MovieId = int.Parse(line[0]),
                    Title = line[1],
                    Genres = new List<string>(line[^1].Split("|"))
                });
            }

            // Return the imported data
            return movies;
        }
        public static IEnumerable<Rating> ImportRatingsCsvData(string filePath)
        {
            // Read the csv file
            var rows = File.ReadAllLines(filePath).Select(x => x.Split(',')).Skip(1);

            // Declare a list for the ratings
            var ratings = new List<Rating>();

            // For each row...
            foreach (var line in rows)
            {
                ratings.Add(new Rating()
                {
                    UserId = int.Parse(line[0]),
                    MovieId = int.Parse(line[1]),
                    Value = float.Parse(line[2], CultureInfo.InvariantCulture),
                    Timestamp = long.Parse(line[3])
                });
            }

            // Return the imported data
            return ratings;
        }
    }
}