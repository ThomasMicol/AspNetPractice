using PRactice.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRactice.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index(int Id = 1)
        {
            Account acc = new Account();
            acc.Id = Id;
            SqlDataReader reader = RunCommand("select * from Accounts where id = '" + Id + "';");
            while (reader.Read())
            {
                acc.Name = reader.GetString(1);
                acc.IsAdmin = reader.GetBoolean(2);
                acc.Rentals = GetRentalsById(Id);
            }
            return View(acc);
        }

        protected List<Rental> GetRentalsById(int id)
        {
            List<Rental> rentals = new List<Rental>();
            SqlDataReader reader = RunCommand("Select * from Rentals where Fk_AccountId = '" + id + "';");
            while(reader.Read())
            {
                Rental aRent = new Rental();
                aRent.Name = reader.GetString(2);
                aRent.HirePeriod = ConvertToHirePeriod(reader.GetInt32(3));
                aRent.HireDate = reader.GetDateTime(4);
                rentals.Add(aRent);
            }
            return rentals;
        }

        protected SqlDataReader RunCommand(string command)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            con.Open();
            SqlCommand com = con.CreateCommand();
            com.CommandText = command;
            return com.ExecuteReader();
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