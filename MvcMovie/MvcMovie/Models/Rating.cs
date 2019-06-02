using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class Rating
    {
        public int RatingID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
