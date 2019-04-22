using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRactice.Models
{
    public class Fib
    {
        public int NumberToCountTo { get; set; }
        public List<int> fibSeq { get; set; }
        public bool isComputed { get; set; }
    }
}