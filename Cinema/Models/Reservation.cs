using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int SessionId { get; set; }      // внешний ключ
        public Session Session{ get; set; }
        public int SeatId{ get; set; }      // внешний ключ
        public Seat Seat{ get; set; }
        public string PersonFirstName { get; set; }
        public string PersonSecondName { get; set; }
    }
}
