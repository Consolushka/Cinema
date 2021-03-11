﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public Session Session{ get; set; }
        public Seat Seat{ get; set; }
        public string PersonFirstName { get; set; }
        public string PersonSecondName { get; set; }
    }
}