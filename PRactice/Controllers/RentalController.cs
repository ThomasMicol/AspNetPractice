using PRactice.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRactice.Controllers
{
    public class RentalController : Controller
    {
        // GET: Rental
        public ActionResult Index()
        {
            return View(GetAllRentals());
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public void AddNewRental(string BookName, string HireLength, int HiredTo)
        {
            InsertNewRental(BookName, ConvertStringToHirePeriod(HireLength), HiredTo);
            Response.Redirect("~/Rental/Index");
        }

        private HirePeriodEnum ConvertStringToHirePeriod(string hirePeriod)
        {
            switch(hirePeriod)
            {
                case "week":
                        return HirePeriodEnum.weekLong;
                case "Single Day":
                        return HirePeriodEnum.singleDay;
                default:
                    return HirePeriodEnum.singleDay;

            }
        }

        private void InsertNewRental(string BookName, HirePeriodEnum p, int HiredTo)
        {
            String command = String.Format("INSERT INTO Rentals (Fk_AccountId, Name, HirePeriod, HireDate) Values ({0}, '{1}', {2}, {3})", HiredTo, BookName, (Int32)p, DateTime.Now.ToShortDateString());
            RunCommand(command); 
        }

        protected List<Rental> GetAllRentals()
        {
            List<Rental> result = new List<Rental>();
            SqlDataReader reader = RunCommand("select * from Rentals");
            while(reader.Read())
            {
                Rental rental = new Rental();
                rental.Name = reader.GetString(2);
                rental.HirePeriod = ConvertToHirePeriod(reader.GetInt32(3));
                rental.HireDate = reader.GetDateTime(4);
                result.Add(rental);
            }
            return result;
        }

        protected SqlDataReader RunCommand(string command)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = command;
                return com.ExecuteReader();
            }catch(Exception e )
            {
                throw e;
            }
            
        }

        protected HirePeriodEnum ConvertToHirePeriod(int Id)
        {
            switch (Id)
            {
                case 0:
                    return HirePeriodEnum.weekLong;
                case 1:
                    return HirePeriodEnum.singleDay;
                default:
                    return HirePeriodEnum.singleDay;
            }
        }
    }
}