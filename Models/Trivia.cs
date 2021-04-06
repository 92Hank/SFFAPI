using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SFFAPI.Models
{
    public class Trivia
    {
        public int Id { get; set; }
        public string TriviaContext { get; set; }
        public int Rating { get; set; }

        public Movie Movie { get; set; }
        public Studio Studio { get; set; }
    }
}