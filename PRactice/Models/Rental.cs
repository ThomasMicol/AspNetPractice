using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRactice.Models
{
    public class Rental
    {
        public string Name { get; set; }
        public HirePeriodEnum HirePeriod { get; set; }
        public DateTime HireDate { get; set; }
    }
}