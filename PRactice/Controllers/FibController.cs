using PRactice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRactice.Controllers
{
    public class FibController : Controller
    {
        // GET: Fib
        public ActionResult Index()
        {
            return View();
        }

        [Route("Fib/{id}")]
        public ActionResult NewFib(int id)
        {
            Fib fibModel = new Fib() { NumberToCountTo = id, fibSeq = GetSeq(id) };
            return View(fibModel);
        }

        [Route("Fib/")]
        public ActionResult GetFib()
        {
            int number = 20;
            Fib fibModel = new Fib() { NumberToCountTo = number, fibSeq = GetSeq(number) };
            return View(fibModel);
        }

        protected List<int> GetSeq(int numberToCountTo)
        {
            List<int> result = new List<int>();
            for(int i = 1; i <= numberToCountTo; i++)
            {
                if (i == 1)
                    result.Add(1);
                if (i == 2)
                    result.Add(2);
                if(i > 2)
                {
                    int numA = result[i - 3];
                    int numB = result[i - 2];
                    result.Add(numA + numB);
                }
            }
            return result;
        }
    }
}