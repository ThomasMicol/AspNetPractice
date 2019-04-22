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
        [Route("Account/{Id}")]
        [Route("Account/")]
        public ActionResult Index(int Id = 1)
        {
            Account acc = new Account();
            acc.Id = Id;
            SqlDataReader reader = RunCommand("select * from Accounts where id = '" + Id + "';");
            while (reader.Read())
            {
                acc.Name = reader.GetString(1);
                acc.IsAdmin = System.Convert.ToBoolean(reader.GetByte(2));
                acc.Rentals = GetRentalsById(Id);
            }
            return View(acc);
        }

        [Route("Account/Create")]
        public ActionResult Create()
        { 
            return View();
        }
        
        [Route("Account/Create/{accountName}/{isAdmin}")]
        public void AddUserAccount(string accountName = "", bool isAdminVal = false)
        {
            string command = String.Format("INSERT INTO Accounts (Name, isAdmin) VALUES ('{0}', {1})", accountName, ConvertToTinyInt(isAdminVal));
            RunCommand(command);
            Response.Redirect("~/Account/Create");
        }

        protected List<Rental> GetRentalsById(int id)
        {
            List<Rental> rentals = new List<Rental>();
            SqlDataReader reader = RunCommand("Select * from Rentals where Fk_AccountsId = '" + id + "';");
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

        protected int ConvertToTinyInt(bool input)
        {
            switch (input)
            {
                case false:
                    return 0;
                case true:
                    return 1;
                default:
                    return 0;
            }

        }

        protected bool ConvertTinyIntToBool(int input)
        {
            switch (input)
            {
                case 1:
                    return true;
                case 0:
                    return false;
                default:
                    return false;
            }

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