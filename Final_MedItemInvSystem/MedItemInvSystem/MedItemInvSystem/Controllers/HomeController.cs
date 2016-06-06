using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

namespace MedItemInvSystem.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to Team 13 Inventory System";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult LogOn2()
        {
            TempData["notice"] = "Enter ID and password.";
            return View();
        }

        public ActionResult LogOff()
        {
            Session["Id"] = null;
            Session["Classification"] = null;

            return View();
        }

        public ActionResult LogInForm(string name, string password)
        {
            for (int k = 0; k < name.Length; k++)
            {
                if (name[k] >= '0' && name[k] <= '9')
                {

                }
                else
                {
                    TempData["notice"] = "Error: Incorrect ID or password.";
                    return View("LogOn2");
                }
            }

            MySqlConnection connection = new MySqlConnection("server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb");
            connection.Open();

            using (connection)
            {
                MySqlCommand command = new MySqlCommand(
                  "SELECT Personnel.LastName, Personnel.Classification, Personnel.EmployeeID FROM Personnel WHERE (Personnel.EmployeeID = '" + name + "') AND (Personnel.Password = '" + password + "')", connection);

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        Session["Id"] = reader[0];
                        Session["EmployeeID"] = reader[2];
                        // Doesn't work if you just assign reader[1].ToString to Session
                        if (reader[1].ToString() == "1")
                        {
                            Session["Classification"] = "Nurse";
                            return View("~/Views/Nurse/Index.aspx");
                        }
                        else if (reader[1].ToString() == "2")
                        {
                            Session["Classification"] = "Supply";
                            return View("~/Views/SupplyManager/Index.aspx");
                        }
                        else if (reader[1].ToString() == "3")
                        {
                            Session["Classification"] = "Doctor";
                            return View("~/Views/Doctor/Index.aspx");
                        }
                        else if (reader[1].ToString() == "4")
                        {
                            Session["Classification"] = "Manager";
                            return View("~/Views/Manager/Index.aspx");
                        }

                    }

                    return View("LogOn2");
                }

                else
                {
                    TempData["notice"] = "Error: Incorrect ID or password.";
                    return View("LogOn2");
                }
            }
        
        }
    }
}
