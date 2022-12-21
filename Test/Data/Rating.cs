using System.Collections.Generic;

namespace Test.Data
{
    public class Rating
    {
        /// <summary>
        /// The unique rating id
        /// </summary>
        public Guid RatingId { get; }

        /// <summary>
        /// The user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The movie id
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// The rating of the movie
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// The time stamp of the rating
        /// </summary>
        public long Timestamp { get; set; }


        /// <summary>
        /// Default constructor
        /// </summary>
        public Rating()
        {
            RatingId = Guid.NewGuid();
        }

    }
}
