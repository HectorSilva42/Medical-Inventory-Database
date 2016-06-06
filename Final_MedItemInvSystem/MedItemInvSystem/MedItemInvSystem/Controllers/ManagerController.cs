using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace MedItemInvSystem.Controllers
{
    public class ManagerController : Controller
    {
        //
        // GET: /Manager/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddUser()
        {
            return View();
        }

        public ActionResult DeleteUser()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult TotalCheckOut()
        {
            Session["EmployeeIDSession"] = null;
            Session["ItemNameSession"] = null;
            Session["QuantitySession"] = null;
            Session["QuantityLessSession"] = null;
            Session["QuantityGreaterSession"] = null;
            Session["QuantityLessThanSession"] = null;
            Session["QuantityGreaterThanSession"] = null;
            Session["AllSession"] = null;
            return View();
        }

        public ActionResult SurgeryRoom()
        {
            Session["EmployeeIDSession"] = null;
            Session["RoomNumberSession"] = null;
            Session["SurgeryIDSession"] = null;
            Session["DeletedSession"] = null;
            Session["AllergenSession"] = null;
            Session["Allergen2Session"] = null;
            Session["Allergen3Session"] = null;
            return View();
        }

        public ActionResult Inventory()
        {
            Session["ItemNameSession"] = null;
            Session["ItemTypeSession"] = null;
            Session["QuantitySession"] = null;
            Session["QuantityLessSession"] = null;
            Session["QuantityGreaterSession"] = null;
            Session["QuantityLessThanSession"] = null;
            Session["QuantityGreaterThanSession"] = null;
            Session["DisposableSession"] = null;
            Session["RefrigeratedSession"] = null;
            Session["InflammableSession"] = null;
            Session["AllergenSession"] = null;
            Session["Allergen2Session"] = null;
            return View();
        }

        public ActionResult ListOfUsers()
        {
            Session["ListOfUsersSession"] = null;
            Session["ClassificationSession"] = null;
            Session["FirstNameSession"] = null;
            Session["LastNameSession"] = null;
            return View();
        }

        public ActionResult CheckOutHistory()
        {
            Session["EmployeeIDSession"] = null;
            Session["ItemNameSession"] = null;
            Session["RoomSession"] = null;
            Session["BackSession"] = null;
            Session["QuantitySession"] = null;
            Session["QuantityLessSession"] = null;
            Session["QuantityGreaterSession"] = null;
            Session["QuantityLessThanSession"] = null;
            Session["QuantityGreaterThanSession"] = null;
            Session["DateSession"] = null;
            return View();
        }

        public ActionResult EmptyListOfUsersForm()
        {
            Session["ListOfUsersSession"] = '%';
            Session["ClassificationSession"] = '%';
            Session["FirstNameSession"] = '%';
            Session["LastNameSession"] = '%';
            return View("ListOfUsers");
        }

        public ActionResult EmptyListOfItemsForm()
        {
            Session["ItemNameSession"] = '%';
            Session["ItemTypeSession"] = '%';
            Session["QuantitySession"] = '%';
            Session["QuantityLessSession"] = "9999999";
            Session["QuantityGreaterSession"] = "-1";
            Session["QuantityLessThanSession"] = "9999999";
            Session["QuantityGreaterThanSession"] = "-1";
            Session["DisposableSession"] = '%';
            Session["RefrigeratedSession"] = '%';
            Session["InflammableSession"] = '%';
            Session["AllergenSession"] = '%';
            Session["Allergen2Session"] = '%';
            return View("Inventory");
        }

        public ActionResult EmptyListOfCheckOutHistoryForm()
        {
            Session["EmployeeIDSession"] = '%';
            Session["ItemNameSession"] = '%';
            Session["RoomSession"] = '%';
            Session["BackSession"] = '%';
            Session["QuantitySession"] = '%';
            Session["QuantityLessSession"] = "9999999";
            Session["QuantityGreaterSession"] = "-1";
            Session["QuantityLessThanSession"] = "9999999";
            Session["QuantityGreaterThanSession"] = "-1";
            Session["DateSession"] = '%';
            return View("CheckOutHistory");
        }

        public ActionResult EmptyListOfRoomsForm()
        {
            Session["EmployeeIDSession"] = '%';
            Session["RoomNumberSession"] = '%';
            Session["SurgeryIDSession"] = '%';
            Session["DeletedSession"] = '%';
            Session["AllergenSession"] = '%';
            Session["Allergen2Session"] = '%';
            Session["Allergen3Session"] = '%';
            return View("SurgeryRoom");
        }

        public ActionResult EmptyTotalForm()
        {
            Session["EmployeeIDSession"] = null;
            Session["ItemNameSession"] = null;
            Session["QuantitySession"] = null;
            Session["QuantityLessSession"] = null;
            Session["QuantityGreaterSession"] = null;
            Session["QuantityLessThanSession"] = null;
            Session["QuantityGreaterThanSession"] = null;
            Session["AllSession"] = '%';
            return View("TotalCheckOut");
        }

        public ActionResult AllergenListOfItemsForm()
        {
            Session["ItemNameSession"] = '%';
            Session["ItemTypeSession"] = '%';
            Session["QuantitySession"] = '%';
            Session["QuantityLessSession"] = "9999999";
            Session["QuantityGreaterSession"] = "-1";
            Session["QuantityLessThanSession"] = "9999999";
            Session["QuantityGreaterThanSession"] = "-1";
            Session["DisposableSession"] = '%';
            Session["RefrigeratedSession"] = '%';
            Session["InflammableSession"] = '%';
            Session["AllergenSession"] = "Plastic";
            Session["Allergen2Session"] = "Latex";
            return View("Inventory");
        }

        public ActionResult AddForm(String first, char middle, String last, int id, String password, int classification)
        {
            if (!AddNewEmployeeForm(classification.ToString()))
            {
                return View("AddUser");
            }

            MySqlConnection connection = new MySqlConnection("server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb");
            connection.Open();

            using (connection)
            {
                MySqlCommand command = new MySqlCommand("SELECT EXISTS (SELECT * FROM Personnel WHERE EmployeeID = '" + id + "')", connection);

                int exists = Convert.ToInt32(command.ExecuteScalar());

                if (exists != 0)
                {
                    TempData["notice"] = "Error: Employee with ID \"" + id + "\" already exists.";
                    return View("AddUser");
                }

                command = new MySqlCommand(
                  "INSERT INTO Personnel VALUES ('" + id + "'" + "," + "'" + first + "'" + "," + "'" + middle + "'" + "," + "'" + last + "'" + "," + "'" + classification + "'" + "," + "'" + password + "')", connection);

                command.ExecuteNonQuery();

                TempData["notice"] = "Successfully added employee " + first + " " + last + " with ID:  " + id;
                return RedirectToAction("AddUser");
            }
        }

        public ActionResult DeleteForm(int id)
        {
            MySqlConnection connection = new MySqlConnection("server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb");
            connection.Open();

            using (connection)
            {
                if (id == (int)Session["EmployeeID"])
                {
                    TempData["notice"] = "Error: Cannot delete self.";
                    return RedirectToAction("DeleteUser");
                }

                MySqlCommand command = new MySqlCommand("SELECT EXISTS (SELECT * FROM Personnel WHERE EmployeeID = '" + id + "')", connection);

                int exists = Convert.ToInt32(command.ExecuteScalar());

                if (exists == 0)
                {
                    TempData["notice"] = "Error: Employee ID does not exist in database.";
                    return RedirectToAction("DeleteUser");
                }

                command = new MySqlCommand("SELECT Classification FROM Personnel WHERE EmployeeID = '" + id + "'", connection);
                int classification = Convert.ToInt32(command.ExecuteScalar());

                if (classification == 1)
                {
                    command = new MySqlCommand("SELECT EXISTS (SELECT * FROM CheckedOut WHERE Personnel_EmployeeID = '" + id + "')", connection);
                    exists = Convert.ToInt32(command.ExecuteScalar());
                    if (exists != 0)
                    {
                        command = new MySqlCommand("SELECT LastName FROM Personnel WHERE EmployeeID = '" + id + "'", connection);
                        String lastName = Convert.ToString(command.ExecuteScalar());
                        TempData["notice"] = "Error: " + lastName + " still has items checked out. Can't delete a user with items checked out.";
                        return RedirectToAction("DeleteUser");
                    }
                }
                if (classification == 3)
                {
                    command = new MySqlCommand("SELECT EXISTS (SELECT * FROM CheckedOut WHERE Personnel_EmployeeID = '" + id + "')", connection);
                    exists = Convert.ToInt32(command.ExecuteScalar());
                    if (exists != 0)
                    {
                        command = new MySqlCommand("SELECT LastName FROM Personnel WHERE EmployeeID = '" + id + "'", connection);
                        String lastName = Convert.ToString(command.ExecuteScalar());
                        TempData["notice"] = "Error: " + lastName + " still has items checked out. Can't delete a user with items checked out.";
                        return RedirectToAction("DeleteUser");
                    }
                    command = new MySqlCommand("SELECT EXISTS (SELECT * FROM SurgeryRoom WHERE EmployeeID = '" + id + "')", connection);
                    exists = Convert.ToInt32(command.ExecuteScalar());
                    if (exists != 0)
                    {
                        command = new MySqlCommand("SELECT LastName FROM Personnel WHERE EmployeeID = '" + id + "'", connection);
                        String lastName = Convert.ToString(command.ExecuteScalar());
                        TempData["notice"] = "Error: " + lastName + " still has rooms checked out. Can't delete a user with rooms checked out.";
                        return RedirectToAction("DeleteUser");
                    }
                }
                if (classification == 4)
                {
                    if ((int)Session["EmployeeID"] == id)
                    {
                        TempData["notice"] = "Error: Cannot delete self.";
                        return RedirectToAction("DeleteUser");
                    }
                }

                command = new MySqlCommand(
                  "DELETE FROM Personnel WHERE (Personnel.EmployeeID = '" + id + "')", connection);
                
                command.ExecuteNonQuery();

                TempData["notice"] = "Successfully removed employee with ID:  " + id;
                return RedirectToAction("DeleteUser");
            }
        }

        public Boolean AddNewEmployeeForm(String classify)
        {
            if (classify.Length != 1)
            {
                TempData["notice"] = "Error. Cannot add user with classification \"" + classify + "\".";
                return false;
            }
                
            if (Convert.ToInt32(classify) < 1 || Convert.ToInt32(classify) > 4)
            {
                TempData["notice"] = "Error. Cannot add user with classification \"" + classify + "\".";
                return false;
            }

            return true;
        }

        //Handles query from text boxes in ListOfUsers.aspx
        public ActionResult ListOfUsersForm(String id, String classification, String firstName, String lastName)
        {
            //Initializing session variables
            Session["ListOfUsersSession"] = id;
            Session["ClassificationSession"] = classification;
            Session["FirstNameSession"] = firstName + "%";
            Session["LastNameSession"] = lastName + "%";

            if (checkNull(id))
            {
                Session["ListOfUsersSession"] = '%';
            }
            if (checkNull(firstName))
            {
                Session["FirstNameSession"] = '%';
            }
            if (checkNull(lastName))
            {
                Session["LastNameSession"] = '%';
            }
            if (checkNull(classification))
            {
                Session["ClassificationSession"] = '%';
            }
            if (classification.Equals("Nurse"))
            {
                Session["ClassificationSession"] = "1";
            }
            if (classification.Equals("Supply Manager"))
            {
                Session["ClassificationSession"] = "2";
            }
            if (classification.Equals("Doctor"))
            {
                Session["ClassificationSession"] = "3";
            }
            if (classification.Equals("Manager"))
            {
                Session["ClassificationSession"] = "4";
            }
            
            MySqlConnection connection = new MySqlConnection("server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb");
            connection.Open();

            //Check if Employee ID exists
            MySqlCommand command = new MySqlCommand("SELECT EXISTS (SELECT * FROM Personnel WHERE EmployeeID = '" + id + "')", connection);

            int exists = Convert.ToInt32(command.ExecuteScalar());

            if (exists == 0)
            {
                if (!checkNull(id))
                {
                    TempData["notice"] = "Error: Employee ID \"" + id + "\" does not exist.";
                    return View("ListOfUsers");
                }
            }

            //Check if Classification exists
            command = new MySqlCommand("SELECT EXISTS (SELECT * FROM enumClass WHERE Title = '" + classification + "')", connection);

            exists = Convert.ToInt32(command.ExecuteScalar());

            if (exists == 0)
            {
                if (!checkNull(classification))
                {
                    TempData["notice"] = "Error: Classification \"" + classification + "\" does not exist.";
                    return View("ListOfUsers");
                }
            }

            return View("ListOfUsers");
        }

        //Function to check if the input string is null
        public Boolean checkNull(String input)
        {
            if (input.Length < 0 || input.Equals("") || input.Equals(null))
            {
                return true;
            }
            return false;
        }

        public ActionResult ListOfItemsForm(String itemName, String itemType, String quantity, Boolean latex, Boolean plastic, String otherAllergens, Boolean disposable, Boolean refrigerated, Boolean inflammable, Boolean penicillin)
        {
            //Initializing session variables
            Session["ItemNameSession"] = itemName + "%";
            Session["ItemTypeSession"] = itemType + "%";
            Session["QuantitySession"] = quantity;
            Session["QuantityLessSession"] = "9999999";
            Session["QuantityGreaterSession"] = "-1";
            Session["QuantityLessThanSession"] = "9999999";
            Session["QuantityGreaterThanSession"] = "-1";
            Session["DisposableSession"] = '%';
            Session["RefrigeratedSession"] = '%';
            Session["InflammableSession"] = '%';
            Session["AllergenSession"] = '%';
            Session["Allergen2Session"] = '%';

            //Checking if arguments are true and assigning appropriate values to session variables  
            if (checkNull(itemName))
            {
                Session["ItemNameSession"] = '%';
            }
            if (checkNull(itemType))
            {
                Session["ItemTypeSession"] = '%';
            }
            if (checkNull(quantity))
            {
                Session["QuantitySession"] = '%';
            }
            if (disposable)
            {
                Session["DisposableSession"] = '1';
            }
            if (refrigerated)
            {
                Session["RefrigeratedSession"] = '1';
            }
            if (inflammable)
            {
                Session["InflammableSession"] = '1';
            }
            if (latex)
            {
                Session["Allergen2Session"] = "Latex";
                Session["AllergenSession"] = "Latex";
            }
            if (penicillin)
            {
                Session["Allergen2Session"] = "Penicillin";
                Session["AllergenSession"] = "Penicillin";
                if (latex)
                {
                    Session["Allergen2Session"] = "Penicillin";
                    Session["AllergenSession"] = "Latex";
                }
            }
            if (plastic)
            {
                Session["Allergen2Session"] = "Plastic";
                Session["AllergenSession"] = "Plastic";
                if (penicillin)
                {
                    Session["Allergen2Session"] = "Penicillin";
                    Session["AllergenSession"] = "Plastic";
                    if (latex)
                    {
                        Session["Allergen2Session"] = "%";
                        Session["AllergenSession"] = "Penicillin";
                    }
                }
                if (latex)
                {
                    Session["Allergen2Session"] = "Plastic";
                    Session["AllergenSession"] = "Latex";
                    if (penicillin)
                    {
                        Session["Allergen2Session"] = "%";
                        Session["AllergenSession"] = "Penicillin";
                    }
                }
            }


            String temp = null;
            //If quantity is not null
            if (!checkNull(quantity))
            {
                //If string contains less than symbol
                if (quantity[0].Equals('<'))
                {
                    //If string contains equals symbol
                    if (quantity[1].Equals('='))
                    {
                        for (int i = 2; i < quantity.Length; i++)
                        {
                            temp += quantity[i];
                        }
                        Session["QuantityLessThanSession"] = temp;
                    }
                    else
                    {
                        for (int i = 1; i < quantity.Length; i++)
                        {
                            temp += quantity[i];
                        }
                        Session["QuantityLessSession"] = temp;
                    }
                    Session["QuantitySession"] = '%';
                }

                //If string contains greater than symbol
                if (quantity[0].Equals('>'))
                {
                    //If string contains equals symbol
                    if (quantity[1].Equals('='))
                    {
                        for (int i = 2; i < quantity.Length; i++)
                        {
                            temp += quantity[i];
                        }
                        Session["QuantityGreaterThanSession"] = temp;
                    }
                    else
                    {
                        for (int i = 1; i < quantity.Length; i++)
                        {
                            temp += quantity[i];
                        }
                        Session["QuantityGreaterSession"] = temp;
                    }
                    Session["QuantitySession"] = '%';
                }
            }
            
            return View("Inventory");
        }

        public ActionResult ListOfCheckOutHistoryForm(String employeeID, String itemName, String roomNumber, String quantity, Boolean back, String date)
        {

            Session["EmployeeIDSession"] = null;
            Session["ItemNameSession"] = null;
            Session["RoomSession"] = null;
            Session["BackSession"] = null;
            Session["QuantitySession"] = null;
            Session["QuantityLessSession"] = null;
            Session["QuantityGreaterSession"] = null;
            Session["QuantityLessThanSession"] = null;
            Session["QuantityGreaterThanSession"] = null;
            Session["DateSession"] = null;

            Session["EmployeeIDSession"] = employeeID;
            Session["ItemNameSession"] = itemName + "%";
            Session["RoomSession"] = roomNumber + "%";
            Session["BackSession"] = '%';
            Session["QuantitySession"] = quantity;
            Session["QuantityLessSession"] = "9999999";
            Session["QuantityGreaterSession"] = "-1";
            Session["QuantityLessThanSession"] = "9999999";
            Session["QuantityGreaterThanSession"] = "-1";
            Session["DateSession"] = date + "%";

            if (checkNull(employeeID))
            {
                Session["EmployeeIDSession"] = '%';
            }
            if (checkNull(itemName))
            {
                Session["ItemNameSession"] = '%';
            }
            if (checkNull(roomNumber))
            {
                Session["RoomSession"] = '%';
            }
            if (back)
            {
                Session["BackSession"] = "1";
            }
            if (checkNull(quantity))
            {
                Session["QuantitySession"] = '%';
            }
            if (checkNull(date))
            {
                Session["DateSession"] = '%';
            }

            String temp = null;
            //If quantity is not null
            if (!checkNull(quantity))
            {
                //If string contains less than symbol
                if (quantity[0].Equals('<'))
                {
                    //If string contains equals symbol
                    if (quantity[1].Equals('='))
                    {
                        for (int i = 2; i < quantity.Length; i++)
                        {
                            temp += quantity[i];
                        }
                        Session["QuantityLessThanSession"] = temp;
                    }
                    else
                    {
                        for (int i = 1; i < quantity.Length; i++)
                        {
                            temp += quantity[i];
                        }
                        Session["QuantityLessSession"] = temp;
                    }
                    Session["QuantitySession"] = '%';
                }

                //If string contains greater than symbol
                if (quantity[0].Equals('>'))
                {
                    //If string contains equals symbol
                    if (quantity[1].Equals('='))
                    {
                        for (int i = 2; i < quantity.Length; i++)
                        {
                            temp += quantity[i];
                        }
                        Session["QuantityGreaterThanSession"] = temp;
                    }
                    else
                    {
                        for (int i = 1; i < quantity.Length; i++)
                        {
                            temp += quantity[i];
                        }
                        Session["QuantityGreaterSession"] = temp;
                    }
                    Session["QuantitySession"] = '%';
                }
            }

            return View("CheckOutHistory");
        }

        public ActionResult ListOfRoomsForm(String employeeID, String roomNumber, String surgeryID, Boolean back, Boolean checkedOut, Boolean latex, Boolean plastic, Boolean penicillin)
        {
            Session["EmployeeIDSession"] = employeeID;
            Session["RoomNumberSession"] = roomNumber + "%";
            Session["SurgeryIDSession"] = surgeryID;
            Session["DeletedSession"] = '%';
            Session["AllergenSession"] = '%';
            Session["Allergen2Session"] = '%';

            if (checkNull(employeeID))
            {
                Session["EmployeeIDSession"] = '%';
            }
            if (checkNull(roomNumber))
            {
                Session["RoomNumberSession"] = '%';
            }
            if (checkNull(surgeryID))
            {
                Session["SurgeryIDSession"] = '%';
            }
            if (back)
            {
                Session["DeletedSession"] = "1";
                if (checkedOut)
                {
                    Session["DeletedSession"] = '%';
                }
            }
            if (checkedOut)
            {
                Session["DeletedSession"] = "0";
                if (back)
                {
                    Session["DeletedSession"] = '%';
                }
            }
            if (latex)
            {
                Session["Allergen2Session"] = "Latex";
                Session["AllergenSession"] = "Latex";
            }
            if (penicillin)
            {
                Session["Allergen2Session"] = "Penicillin";
                Session["AllergenSession"] = "Penicillin";
                if (latex)
                {
                    Session["Allergen2Session"] = "Penicillin";
                    Session["AllergenSession"] = "Latex";
                }
            }
            if (plastic)
            {
                Session["Allergen2Session"] = "Plastic";
                Session["AllergenSession"] = "Plastic";
                if (penicillin)
                {
                    Session["Allergen2Session"] = "Penicillin";
                    Session["AllergenSession"] = "Plastic";
                    if (latex)
                    {
                        Session["Allergen2Session"] = "%";
                        Session["AllergenSession"] = "Penicillin";
                    }
                }
                if (latex)
                {
                    Session["Allergen2Session"] = "Plastic";
                    Session["AllergenSession"] = "Latex";
                    if (penicillin)
                    {
                        Session["Allergen2Session"] = "%";
                        Session["AllergenSession"] = "Penicillin";
                    }
                }
            }

            return View("SurgeryRoom");
        }

        public ActionResult TotalForm(String employeeID, String itemName, String quantity)
        {
            Session["EmployeeIDSession"] = employeeID;
            Session["ItemNameSession"] = itemName + "%";
            Session["QuantitySession"] = quantity;
            Session["QuantityLessSession"] = "9999999";
            Session["QuantityGreaterSession"] = "-1";
            Session["QuantityLessThanSession"] = "9999999";
            Session["QuantityGreaterThanSession"] = "-1";
            Session["AllSession"] = "CheckedOut.Inventory_ItemName";

            if (checkNull(employeeID))
            {
                Session["EmployeeIDSession"] = '%';
            }
            if (checkNull(itemName))
            {
                Session["ItemNameSession"] = '%';
            }
            //if (checkNull(quantity))
            //{
            //    Session["QuantitySession"] = '%';
            //}

            //String temp = null;
            ////If quantity is not null
            //if (!checkNull(quantity))
            //{
            //    //If string contains less than symbol
            //    if (quantity[0].Equals('<'))
            //    {
            //        //If string contains equals symbol
            //        if (quantity[1].Equals('='))
            //        {
            //            for (int i = 2; i < quantity.Length; i++)
            //            {
            //                temp += quantity[i];
            //            }
            //            Session["QuantityLessThanSession"] = temp;
            //        }
            //        else
            //        {
            //            for (int i = 1; i < quantity.Length; i++)
            //            {
            //                temp += quantity[i];
            //            }
            //            Session["QuantityLessSession"] = temp;
            //        }
            //        Session["QuantitySession"] = '%';
            //    }

            //    //If string contains greater than symbol
            //    if (quantity[0].Equals('>'))
            //    {
            //        //If string contains equals symbol
            //        if (quantity[1].Equals('='))
            //        {
            //            for (int i = 2; i < quantity.Length; i++)
            //            {
            //                temp += quantity[i];
            //            }
            //            Session["QuantityGreaterThanSession"] = temp;
            //        }
            //        else
            //        {
            //            for (int i = 1; i < quantity.Length; i++)
            //            {
            //                temp += quantity[i];
            //            }
            //            Session["QuantityGreaterSession"] = temp;
            //        }
            //        Session["QuantitySession"] = '%';
            //    }
            //}

            return View("TotalCheckOut");
        }
    }
}
