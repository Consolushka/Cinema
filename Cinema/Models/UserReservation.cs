using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class UserReservation
    {
        public int Session { get; set; }
        public List<int> ReservedSeats { get; set; }
        public string FName { get; set; }
        public string SName { get; set; }

}
}
