using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SFFAPI.Data;

namespace SFFAPI.Models
{
    public class Label
    {
        public string Movie { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }

        public async Task<Label> CreateLabel(DataContext _context, int movieId, int studioId)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            var studio = await _context.Studios.FindAsync(studioId);

            var label = new Label() { Movie = movie.Title, City = studio.City, Date = DateTime.Now };

            return label;
        }

        public Label LabelData(Label label)
        {
            return label;
        }
    }
}