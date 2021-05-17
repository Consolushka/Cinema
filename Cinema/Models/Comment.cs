using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int MovieId { get; set;}
        public Movie Movie { get; set; }
        public string PersonName { get; set; }
        public string Text { get; set; }
    }
}
