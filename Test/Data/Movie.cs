using System.Collections.Generic;

namespace Test.Data
{
    public class Movie
    {
        /// <summary>
        /// The id of the movie
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// The title of the movie
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The genres that the movie is associated with
        /// </summary>
        public List<string> Genres { get; set; }

    }
}
