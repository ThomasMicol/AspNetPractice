using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRactice.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public List<Rental> Rentals { get; set; }
    }
}