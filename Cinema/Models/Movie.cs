using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set;}
        public string Desc { get; set; }
        public string Director { get; set; }
        public string Poster { get; set;}
        public string Rating { get; set;}
        public DateTime ReleaseDate { get; set; }
    }
}
