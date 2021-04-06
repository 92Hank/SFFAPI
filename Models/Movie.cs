using System;
using System.Linq;
using System.Collections.Generic;

namespace SFFAPI.Models
{
    public enum Genre
    {
        Action,
        SciFi,
        Comedy,
        Documentary
    }
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public int Quantity { get; set; }
        public ICollection<Trivia> Trivias { get; set; } = new List<Trivia>();

        public void AddTrivia(Trivia trivia, Studio studio)
        {
            trivia.Studio = studio;
            Trivias.Add(trivia);
        }
    }
}