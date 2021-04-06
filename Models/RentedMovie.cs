namespace SFFAPI.Models
{
    public class RentedMovie
    {
        public int RentedMovieId { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}