using System;
using System.Linq;
using System.Collections.Generic;
using SFFAPI.Data;

namespace SFFAPI.Models
{
    public class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public ICollection<RentedMovie> RentedMovies { get; set; } = new List<RentedMovie>();

        public void AddMovie(Movie movie)
        {
            if (movie.Quantity > 0)
            {
                movie.Quantity--;

                RentedMovie rented = new RentedMovie() { Movie = movie };
                RentedMovies.Add(rented);
            }
        }

        public RentedMovie ReturnMovie(int id)
        {
            var rentedMovie = RentedMovies.Where(m => m.MovieId == id).FirstOrDefault();
            var movie = RentedMovies.Select(m => m.Movie).Where(m => m.Id == id).FirstOrDefault();

            if (rentedMovie != null)
            {
                RentedMovies.Remove(rentedMovie);
                movie.Quantity++;
            }

            return rentedMovie;
        }
    }
}