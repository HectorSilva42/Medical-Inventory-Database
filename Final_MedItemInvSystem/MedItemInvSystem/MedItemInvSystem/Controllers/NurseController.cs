﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Net.Mail;
using System.Net;

namespace MedItemInvSystem.Controllers
{
    public class NurseController : Controller
    {
        //
        // GET: /Nurse/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckInItem()
        {
            return View();
        }

        public ActionResult CheckOutItem()
        {
            return View();
        }

        public ActionResult CheckOutHistory()
        {
            return View();
        }

        public ActionResult Inventory()
        {
            return View();
        }

        public ActionResult Rooms()
        {
            return View();
        }

        public ActionResult CheckInItemForm(String itemName, int? id, int? quantity, int? checkOutNum)
        {
            //Check if quantity to check in is less than or equal to 0
            if (quantity <= 0)
            {
                TempData["notice"] = "Error: Invalid check-in quantity \"" + quantity + "\". Please enter a valid number.";
                return View("CheckInItem");
            }

            MySqlConnection connection = new MySqlConnection("server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb");
            connection.Open();

            using (connection)
            {
                //Check if check out # exists for this employee
                MySqlCommand command = new MySqlCommand("SELECT EXISTS (SELECT * FROM CheckedOut WHERE IndexKey = '" + checkOutNum + "' AND Personnel_EmployeeID = '" + (int)Session["EmployeeID"] + "')", connection);

                int exists = Convert.ToInt32(command.ExecuteScalar());

                if (exists == 0)
                {
                    TempData["notice"] = "Error: Check Out # does not exist in your check out history.";
                    return View("CheckInItem");
                }

                //Check if the item id exists in inventory
                command = new MySqlCommand("SELECT EXISTS (SELECT * FROM Inventory WHERE ItemID = '" + id + "')", connection);

                exists = Convert.ToInt32(command.ExecuteScalar());

                if (exists == 0)
                {
                    TempData["notice"] = "Error: Incorrect Item ID. \"" + id + "\" does not exist in the inventory.";
                    return View("CheckInItem");
                }

                //Check if the item name exists in inventory
                command = new MySqlCommand("SELECT EXISTS (SELECT * FROM Inventory WHERE ItemName = '" + itemName + "')", connection);

                exists = Convert.ToInt32(command.ExecuteScalar());

                if (exists == 0)
                {
                    TempData["notice"] = "Error: Incorrect Item Name. \"" + itemName + "\" does not exist in the inventory.";
                    return View("CheckInItem");
                }

                //Check if the item name and item id combination exists in inventory
                command = new MySqlCommand("SELECT EXISTS (SELECT * FROM Inventory WHERE ItemName = '" + itemName + "' AND ItemID = '" + id + "')", connection);

                exists = Convert.ToInt32(command.ExecuteScalar());

                if (exists == 0)
                {
                    TempData["notice"] = "Error: Mismatching Item Name \"" + itemName + "\" and Item ID \"" + id + "\". Combination of item name and ID does not exist in the inventory.";
                    return View("CheckInItem");
                }

                //Check if the item being checked in exists in the history of checked out items for this employee
                command = new MySqlCommand("SELECT EXISTS (SELECT * FROM CheckedOut WHERE Personnel_EmployeeID = '" + (int)Session["EmployeeID"] + "' AND Inventory_ItemName = '" + itemName + "' AND QuantityCheckedOut = '" + quantity + "')", connection);

                exists = Convert.ToInt32(command.ExecuteScalar());

                if (exists == 0)
                {
                    TempData["notice"] = "Error: Mismatching Item Name \"" + itemName + "\", Item ID \"" + id + "\" or Quantity \"" + quantity + "\". Combination of item name, ID, and quantity does not exist in your checked out history.";
                    return View("CheckInItem");
                }

                //Check if the item being checked in has not been deleted in the check out history for this employee
                command = new MySqlCommand("SELECT EXISTS (SELECT * FROM CheckedOut WHERE Personnel_EmployeeID = '" + (int)Session["EmployeeID"] + "' AND Inventory_ItemName = '" + itemName + "' AND QuantityCheckedOut = '" + quantity + "' AND Deleted = '" + 0 + "')", connection);

                exists = Convert.ToInt32(command.ExecuteScalar());

                if (exists == 0)
                {
                    TempData["notice"] = "Error: Cannot check in items that are not checked out.";
                    return View("CheckInItem");
                }

                int deleted = 0;

                //Check if the item being checked in matches with the given index key
                command = new MySqlCommand("SELECT EXISTS (SELECT * FROM CheckedOut WHERE IndexKey = '" + checkOutNum + "' AND Personnel_EmployeeID = '" + (int)Session["EmployeeID"] + "' AND Inventory_ItemName = '" + itemName + "' AND QuantityCheckedOut = '" + quantity + "' AND Deleted = '" + deleted + "')", connection);

                exists = Convert.ToInt32(command.ExecuteScalar());

                if (exists == 0)
                {
                    TempData["notice"] = "Error: Index Key does not match.";
                    return View("CheckInItem");
                }

                //Update the inventory table to reflect an item being checked back in
                command = new MySqlCommand(
                  "UPDATE Inventory SET QuantityAvailable = QuantityAvailable + '" + quantity + "' WHERE itemID = '" + id + "'", connection);

                command.ExecuteNonQuery();

                deleted = 1;

                command = new MySqlCommand("UPDATE CheckedOut SET CheckedOut.Deleted = '" + deleted + "' WHERE IndexKey = '" + checkOutNum + "' AND Personnel_EmployeeID = '" + (int)Session["EmployeeID"] + "' AND Inventory_ItemName = '" + itemName + "' AND QuantityCheckedOut = '" + quantity + "'", connection);

                command.ExecuteNonQuery();

                TempData["notice"] = "Successfully checked in " + itemName + ". Quantity Checked In: " + quantity;
                return RedirectToAction("CheckInItem");
            }
        }

        public ActionResult CheckOutItemForm(String itemName, int id, int employeeID, DateTime date, int surgeryID, String room, int quantity)
        {
            //Check if employee id entered matches current employee id logged in
            if (employeeID != (int)Session["EmployeeID"])
            {
                TempData["notice"] = "Error: Incorrect Employee ID \"" + employeeID + "\".";
                return View("CheckOutItem");
            }

            //Check if checked out quantity is greater than 30
            if (quantity > 30)
            {
                TempData["notice"] = "Error: Cannot check out more than 30 of any item at once.";
                return View("CheckOutItem");
            }

            //Check if quantity is less than or equal to 0
            if (quantity <= 0)
            {
                TempData["notice"] = "Error: Invalid check-out quantity \"" + quantity + "\". Please enter a valid number.";
                return View("CheckOutItem");
            }

            MySqlConnection connection = new MySqlConnection("server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb");
            connection.Open();

            //Check if Item ID exists in inventory
            MySqlCommand command = new MySqlCommand("SELECT EXISTS (SELECT * FROM Inventory WHERE ItemID = '" + id + "')", connection);

            int exists = Convert.ToInt32(command.ExecuteScalar());

            if (exists == 0)
            {
                TempData["notice"] = "Error: Incorrect Item ID. \"" + id + "\" does not exist in the inventory.";
                return View("CheckOutItem");
            }

            //Check if Item Name exists in inventory
            command = new MySqlCommand("SELECT EXISTS (SELECT * FROM Inventory WHERE ItemName = '" + itemName + "')", connection);

            exists = Convert.ToInt32(command.ExecuteScalar());

            if (exists == 0)
            {
                TempData["notice"] = "Error: Incorrect Item Name. \"" + itemName + "\" does not exist in the inventory.";
                return View("CheckOutItem");
            }

            //Check if Item Name and Item ID combination exists in inventory
            command = new MySqlCommand("SELECT EXISTS (SELECT * FROM Inventory WHERE ItemName = '" + itemName + "' AND ItemID = '" + id + "')", connection);

            exists = Convert.ToInt32(command.ExecuteScalar());

            if (exists == 0)
            {
                TempData["notice"] = "Error: Mismatching Item Name \"" + itemName + "\" and Item ID \"" + id + "\". Combination of item name and ID does not exist in the inventory.";
                return View("CheckOutItem");
            }

            //Check if Surgery ID room checked out to exists
            command = new MySqlCommand("SELECT EXISTS (SELECT * FROM SurgeryRoom WHERE SurgeryID = '" + surgeryID + "')", connection);

            exists = Convert.ToInt32(command.ExecuteScalar());

            if (exists == 0)
            {
                TempData["notice"] = "Error: Incorrect Surgery ID. \"" + surgeryID + "\" does not exist.";
                return View("CheckOutItem");
            }

            //Check if the quantity available for that item is greater than 0 or if checked out quantity exceeds available
            command = new MySqlCommand("SELECT QuantityAvailable FROM Inventory WHERE ItemID = '" + id + "'", connection);
            int numAvailable = Convert.ToInt32(command.ExecuteScalar());

            if (numAvailable <= 0)
            {
                TempData["notice"] = "Error: Quantity available is 0.";
                return View("CheckOutItem");
            }
            else if (numAvailable < quantity)
            {
                TempData["notice"] = "Error: Quantity to check out exceeds quantity available.";
                return View("CheckOutItem");
            }

            //Check if surgery room is associated with given surgery id
            command = new MySqlCommand("SELECT EXISTS (SELECT * FROM SurgeryRoom WHERE RoomNumber = '" + room + "' AND SurgeryID = '" + surgeryID + "')", connection);

            exists = Convert.ToInt32(command.ExecuteScalar());
            if (exists == 0)
            {
                TempData["notice"] = "Error: Surgery ID \"" + surgeryID + "\" is not associated with Room Number \"" + room + "\".";
                return View("CheckOutItem");
            }

            //Check if item being checked out has an allergen
            command = new MySqlCommand("SELECT Allergen FROM Inventory WHERE ItemName = '" + itemName + "'", connection);

            String allergen = Convert.ToString(command.ExecuteScalar());

            if (allergen != null && allergen.Length > 0)
            {
                //Check if the allergen is associated with that room
                command = new MySqlCommand("SELECT RoomAllergens FROM SurgeryRoom WHERE RoomNumber = '" + room + "'", connection);
                String roomAllergen = Convert.ToString(command.ExecuteScalar());
                if (!roomAllergen.Equals(null) && roomAllergen.Length > 0)
                {
                    if (roomAllergen.Equals(allergen) && roomAllergen != null && allergen != null)
                    {
                        TempData["Notice"] = "Error: Can't check out \"" + itemName + "\" to Room \"" + room + "\". Item Allergen \"" + allergen + "\" is associated with the room.";
                        return View("CheckOutItem");
                    }
                }
            }

            command = new MySqlCommand("SELECT Deleted FROM SurgeryRoom WHERE SurgeryID = '" + surgeryID + "'", connection);
            exists = Convert.ToInt32(command.ExecuteScalar());

            if (exists == 1)
            {
                TempData["notice"] = "Error: Can't check out to a room that is checked in.";
                return View("CheckOutItem");
            }

            //Add tuple to checked out table
            command = new MySqlCommand(
                  "INSERT INTO CheckedOut VALUES ('IndexKey + 1', '" + date.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + employeeID + "', '" + room + "', '" + itemName + "', '"
                  + surgeryID + "', '" + quantity + "', '" + 0 + "')", connection);
            //Update quantity in inventory
            MySqlCommand updateCommand = new MySqlCommand("UPDATE Inventory SET QuantityAvailable = QuantityAvailable - '" + quantity + "' WHERE ItemID = '" + id + "'", connection);

            command.ExecuteNonQuery();
            updateCommand.ExecuteNonQuery();

            if (numAvailable >= 200 && numAvailable - quantity < 200)
            {
                var fromAddress = new MailAddress("s.r.testAddress@gmail.com", "Team 13 Medical System");
                var toAddress = new MailAddress("silva42.hector@gmail.com", "Manager");
                const string fromPassword = "testPassword";
                const string subject = "Low Stock Warning";


                string body = "We Need to Order more " + itemName;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

            }

            TempData["notice"] = "Successfully checked out " + itemName + ". Quantity Checked Out: " + quantity + ". Room: " + room;
            return RedirectToAction("CheckOutItem");
        }
    }
}
