using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
        public double Rating { get; set;}
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
    }
}
