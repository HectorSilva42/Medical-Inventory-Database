using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;

namespace MedItemInvSystem.Controllers
{
    public class SupplyManagerController : Controller
    {
        //
        // GET: /SupplyManager/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReorderItems()
        {
            return View();
        }

        public ActionResult ViewInventoryReport()
        {
            return View();
        }

        public ActionResult CheckIn()
        {
            TempData["notice"] = "Please enter the Item's Name or ID";
            return View();
        }

        public ActionResult AddItem()
        {
            return View();
        }

        public ActionResult AddItemPage2()
        {
            TempData["notice"] = null;
            return View();
        }

        public ActionResult AddItemSuccess()
        {
            TempData["notice"] = "The Item " + Session["ItemNameTemp_AddForm"] + " Has Been Successfully Added to Inventory";
            return View("AddItemSuccess");
        }

        public ActionResult RestockItems()
        {
            return View();
        }

        public ActionResult ReorderForm(string ItemID, string Quantity)
        {
            if (ItemID.Length == 0)
            {
                TempData["notice"] = "Error: Please Enter Item ID";
                return View("RestockItems");
            }
            for (int k = 0; k < ItemID.Length; k++)
            {
                if (ItemID[k] >= '0' && ItemID[k] <= '9')
                {

                }
                else
                {
                    if (ItemID.Length != 3)
                        TempData["notice"] = "Error: Invalid Item ID and Item ID needs to be 3 digits.";
                    else
                        TempData["notice"] = "Error: Invalid Item ID";
                    return View("RestockItems");
                }
            }
            if (ItemID.Length != 3)
            {
                TempData["notice"] = "Error: Please Enter a Item ID With 3 Digits";
                return View("RestockItems");
            }

            if (Quantity.Length == 0)
            {
                TempData["notice"] = "Error: Please Enter Quantity";
                return View("RestockItems");
            }
            for (int k = 0; k < Quantity.Length; k++)
            {
                if (Quantity[k] >= '0' && Quantity[k] <= '9')
                {

                }
                else
                {
                    TempData["notice"] = "Error: Invalid Quantity";
                    return View("RestockItems");
                }
            }

            //This reorders Items and updates the disposable items with the new value. This needs to be updated.
            MySqlConnection connection = new MySqlConnection("server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb");
            connection.Open();
            String order = "";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(
                  "SELECT Inventory.ItemName,Cost.Cost1 FROM mydb.Inventory INNER JOIN mydb.Cost ON Inventory.ItemName = Cost.ItemDescription_Inventory_ItemName WHERE Inventory.ItemID = " + ItemID, connection);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    order += "We need to order " + Quantity + " of " + reader[0] +"\nThat will cost "+reader[1]+" per an item";
                    ViewData["Order"] = "You Have Ordered " + Quantity + " of " + reader[0];
                }
            }
            
            var fromAddress = new MailAddress("s.r.testAddress@gmail.com", "Team 13 Medical System");
            var toAddress = new MailAddress("silva42.hector@gmail.com", "Manager");
            const string fromPassword = "testPassword";
            const string subject = "Order Form";
            string body = order;

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
            return View("ReorderItems");
        }

        public ActionResult AddForm(String itemName, String itemID, String itemType, bool disp, String quantity, String allergen)
        {
            int dispType = 0;
            if(disp)
                dispType = 1;
            Session["dispTemp_AddForm"] = dispType;
            TempData["notice"] = "";
            //CheckName
            if (itemName.Length == 0)
            {
                TempData["notice"] = "Error: Please Enter Item Name";
                return View("AddItem");
            }
            Session["ItemNameTemp_AddForm"] = itemName;
            //
            //Check item ID
            //
            if (itemID.Length == 0)
            {
                TempData["notice"] = "Error: Please Enter Item ID";
                return View("AddItem");
            }
            for (int k = 0; k < itemID.Length; k++)
            {
                if (itemID[k] >= '0' && itemID[k] <= '9')
                {

                }
                else
                {
                    if(itemID.Length != 3)
                        TempData["notice"] = "Error: Invalid Item ID and Item ID needs to be 3 digits.";
                    else
                        TempData["notice"] = "Error: Invalid Item ID";
                    return View("AddItem");
                }
            }
            if (itemID.Length != 3)
            {
                TempData["notice"] = "Error: Please Enter Item ID";
                return View("AddItem");
            }
            TempData["notice"] = "Item ID " + itemID + " was accepted";
            //Checktype
            if (itemType.Length == 0)
            {
                TempData["notice"] = "Error: Please Enter Item Type";
                return View("AddItem");
            }
            //
            //Check item quantity
            //
            if (quantity.Length == 0)
            {
                TempData["notice"] = "Error: Please Enter Quantity";
                return View("AddItem");
            }
            for (int k = 0; k < quantity.Length; k++)
            {
                if (quantity[k] >= '0' && quantity[k] <= '9')
                {

                }
                else
                {
                    TempData["notice"] = "Error: Please enter Valid Quantity";
                    return View("AddItem");
                }
            }
            //connect to server and check if item already exist
            MySqlConnection connection = new MySqlConnection("server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb");
            connection.Open();

            using (connection)
            {
                MySqlCommand command = new MySqlCommand(
                  "SELECT Inventory.ItemName, Inventory.ItemID FROM Inventory WHERE (Inventory.ItemName = '" + itemName + "')", connection);

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        TempData["notice"] = "Item " + reader[0] + " already use ID of " + reader[1]+". Please go to \"Restock Items\" to add more.";
                        return View("AddItem");
                    }
                }

                command = new MySqlCommand(
                  "SELECT Inventory.ItemName, Inventory.ItemID FROM Inventory WHERE (Inventory.ItemID = '" + itemID + "')", connection);
                reader.Close();
                reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        TempData["notice"] = "Item " + reader[0] + " already has ID of " + reader[1];
                        return View("AddItem");
                    }
                }
                reader.Close();
                
                command = new MySqlCommand(
                "INSERT INTO Inventory VALUES ('" + itemName + "'" + "," + "'" + itemType + "'" + "," + "'" + quantity + "'" + "," + "'" + dispType + "'" + "," + "'" + itemID + "'" + "," + "'" + allergen + "')", connection);

                command.ExecuteNonQuery();
                
                TempData["notice"] = "Successfully added " + quantity + " of " + itemName;
                return RedirectToAction("AddItemPage2");
            }
        }

        public ActionResult RestockForm(String itemID, String quantity)
        {
            //
            //Check item ID
            //
            if (itemID.Length == 0)
            {
                TempData["notice"] = "Error: Please Enter Item ID";
                return View("RestockItems");
            }
            for (int k = 0; k < itemID.Length; k++)
            {
                if (itemID[k] >= '0' && itemID[k] <= '9')
                {

                }
                else
                {
                    if (itemID.Length != 3)
                        TempData["notice"] = "Error: Invalid Item ID and Item ID needs to be 3 digits.";
                    else
                        TempData["notice"] = "Error: Invalid Item ID";
                    return View("RestockItems");
                }
            }
            if (itemID.Length != 3)
            {
                TempData["notice"] = "Error: Please Enter a Item ID With 3 Digits";
                return View("RestockItems");
            }

            if (quantity.Length == 0)
            {
                TempData["notice"] = "Error: Please Enter Quantity";
                return View("RestockItems");
            }
            for (int k = 0; k < quantity.Length; k++)
            {
                if (quantity[k] >= '0' && quantity[k] <= '9')
                {

                }
                else
                {
                    TempData["notice"] = "Error: Invalid Quantity";
                    return View("RestockItems");
                }
            }

            String itemName = "";
            MySqlConnection connection = new MySqlConnection("server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb");
            connection.Open();
            
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(
                  "SELECT Inventory.ItemName FROM Inventory WHERE (Inventory.ItemID = '" + itemID + "')", connection);

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        command = new MySqlCommand(
                          "UPDATE Inventory SET Inventory.QuantityAvailable = Inventory.QuantityAvailable + " + quantity + " WHERE Inventory.ITEMID = " + itemID, connection);
                        itemName = ""+reader[0];
                        reader.Close();
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    TempData["notice"] = "Item ID "+itemID+" Does Not Exist in System";
                    return View("RestockItems");
                }
                TempData["notice"] = "Successfully Stocked " + quantity + " " + itemName;
                return RedirectToAction("RestockItems");
            }
        }

        public ActionResult CheckInItem(String choice, String item,String surgID,String quantity)
        {
            //check name
            if (item.Length == 0)
            {
                if (choice[0] == '1')
                    TempData["notice"] = "Please Enter Value in the ID Field";
                else
                    TempData["notice"] = "Please Enter Value in the Name Field";
                return View("CheckIn");
            }
            if (surgID.Length == 0)
            {
                TempData["notice"] = "Please Enter a Value in the Surgury ID Field";
                return View("CheckIn");
            }
            if (quantity.Length == 0)
            {
                TempData["notice"] = "Please Enter How Many Items You Are Returning";
                return View("CheckIn");
            }
            for (int k = 0; k < quantity.Length; k++)
            {
                if (quantity[k] >= '0' && quantity[k] <= '9')
                {

                }
                else
                {
                    TempData["notice"] = "Error: Invalid Quantity";
                    return View("CheckIn");
                }
            }
            //Check surgury ID
            for (int k = 0; k < surgID.Length; k++)
            {
                if (surgID[k] >= '0' && surgID[k] <= '9')
                {

                }
                else
                {
                    if (surgID.Length != 4)
                        TempData["notice"] = "Error: Invalid Surgury ID, and Surgury ID needs to be 4 digits.";
                    else
                        TempData["notice"] = "Error: Invalid Surgury ID";
                    return View("CheckIn");
                }
            }
            if (surgID.Length != 4)
            {
                TempData["notice"] = "Error: Please Enter 4 Digit Surgury ID";
                return View("CheckIn");
            }
            
            //ID
            if (choice[0] == '1')
            {
                for (int k = 0; k < item.Length; k++)
                {
                    if (item[k] >= '0' && item[k] <= '9')
                    {

                    }
                    else
                    {
                        if (item.Length != 3)
                            TempData["notice"] = "Error: Invalid Item ID and Item ID needs to be 3 digits.";
                        else
                            TempData["notice"] = "Error: Invalid Item ID";
                        return View("CheckIn");
                    }
                }
                if (item.Length != 3)
                {
                    TempData["notice"] = "Error: Please Enter Item ID";
                    return View("CheckIn");
                }
                
                //connect to server and check if item already exist
                MySqlConnection connection = new MySqlConnection("server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb");
                connection.Open();

                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                      "SELECT Inventory.Disposable, Inventory.ItemName FROM Inventory WHERE (Inventory.ItemID = '" + item + "')", connection);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            String disp = "" + reader[0];
                            if (disp.Equals("True"))
                            {
                                TempData["notice"] = "Item " + reader[1] + " is a disposable item and can not be checked in. Please follow apropriate disposal procedure.";
                                return View("CheckIn");
                            }
                            else
                            {
                                //Check that Quantity matches
                                String ItemName = "" + reader[1];
                                reader.Close();
                                command = new MySqlCommand(
                                    "SELECT CheckedOut.QuantityCheckedOut, CheckedOut.IndexKey FROM CheckedOut WHERE (CheckedOut.Deleted = '0') AND (CheckedOut.Inventory_ItemName = '" + ItemName + "') AND (CheckedOut.SurgeryRoom_SurgeryID = '" + surgID + "')", connection);
                                MySqlDataReader quantityCheck = command.ExecuteReader();
                                if (quantityCheck.HasRows)
                                {
                                    if (quantityCheck.Read())
                                        if (quantityCheck[0].Equals(quantity))
                                        {
                                            command = new MySqlCommand(
                                                "UPDATE Inventory SET Inventory.QuantityAvailable = Inventory.QuantityAvailable+1 WHERE Inventory.ItemName = '" + ItemName + "'", connection);
                                            command.ExecuteNonQuery();
                                            command = new MySqlCommand(
                                                "UPDATE CheckedOut SET CheckedOut.Deleted = '1' WHERE  CheckedOut.IndexKey = '" + quantityCheck[1] + "'", connection);
                                            command.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            TempData["notice"] = "You Must Return All "+disp+"'s at one Time";
                                            return View("CheckIn");
                                        }
                                }
                                else
                                {
                                    TempData["notice"] = "Could Not Find That Item Currently Checked Out For That Surgury";
                                    return View("CheckIn");
                                }

                                TempData["notice"] = "The Item ID" + item + " Has Been Checked In";
                            }
                            return View("CheckIn");
                        }
                    }
                    else
                        TempData["notice"] = "The item ID" + item + " is invalid";
                    return View("CheckIn");
                }
            }
            //Name
            else if (choice[0] == '2')
            {
                //connect to server and check if item already exist
                MySqlConnection connection = new MySqlConnection("server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb");
                connection.Open();

                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                      "SELECT Inventory.Disposable, Inventory.ItemName FROM Inventory WHERE (Inventory.ItemName = '" + item + "')", connection);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            String disp = "" + reader[0];
                            if (disp.Equals("True"))
                            {
                                TempData["notice"] = "Item " + reader[1] + " is a disposable item and can not be checked in. Please follow apropriate disposal procedure.";
                                return View("CheckIn");
                            }
                            else
                            {
                                String ItemName = ""+reader[1];
                                reader.Close();
                                //Check that Quantity matches
                                command = new MySqlCommand(
                                    "SELECT CheckedOut.QuantityCheckedOut, CheckedOut.IndexKey FROM CheckedOut WHERE (CheckedOut.Deleted = '0') AND (CheckedOut.Inventory_ItemName = '" + ItemName + "') AND (CheckedOut.SurgeryRoom_SurgeryID = '" + surgID + "')", connection);
                                MySqlDataReader quantityCheck = command.ExecuteReader();
                                if (quantityCheck.HasRows)
                                {
                                    if (quantityCheck.Read())
                                        if (quantityCheck[0].Equals(quantity))
                                        {
                                            command = new MySqlCommand(
                                                "UPDATE Inventory SET Inventory.QuantityAvailable = Inventory.QuantityAvailable+1 WHERE Inventory.ItemName = '" + ItemName + "'", connection);
                                            command.ExecuteNonQuery();
                                            command = new MySqlCommand(
                                                "UPDATE CheckedOut SET CheckedOut.Deleted = '1' WHERE  CheckedOut.IndexKey = '" + quantityCheck[1] + "'", connection);
                                            command.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            TempData["notice"] = "You Must Return All of One Type of Item at a Time";
                                            return View("CheckIn");
                                        }
                                }
                                else
                                {
                                    TempData["notice"] = "Could Not Find That Item Currently Checked Out For That Surgury";
                                    return View("CheckIn");
                                }

                                TempData["notice"] = "The Item " + item + " Has Been Checked In";
                            }
                            return View("CheckIn");
                        }
                    }
                    else
                        TempData["notice"] = "The item " + item + " is invalid";
                    return View("CheckIn");
                }
            }
            //error
            else
                TempData["notice"] = "Unknown error";
            return View("CheckIn");
        }

        public ActionResult TestButton(String itemId,String Quantity)
        {
            TempData["notice"] = itemId + " " + Quantity;
            var fromAddress = new MailAddress("from@gmail.com", "From Name");
            var toAddress = new MailAddress("to@example.com", "To Name");
            const string fromPassword = "fromPassword";
            const string subject = "Subject";
            const string body = "Body";

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
            return View("ReorderItems");
        }

        public ActionResult AddFormDiscription(String size,String itemUse,String itemDisc, Boolean inflam, Boolean refrig, String cost)
        {
            int inflamType = 0;
            if (inflam)
                inflamType = 1;
            int refrigType = 0;
            if (refrig)
                refrigType = 1;
            if (itemUse.Length == 0)
            {
                TempData["notice"] = "Error: Please Enter the Item's Use";
                return View("AddItemPage2");
            }
            if (cost.Length == 0)
            {
                TempData["notice"] = "Error: Please enter the cost.";
                return View("AddItemPage2");
            }

            //connect to server and check if item already exist
            MySqlConnection connection = new MySqlConnection("server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb");
            connection.Open();

            using (connection)
            {
                if (!size.Equals("null") && itemDisc.Length > 0)
                {
                    MySqlCommand command = new MySqlCommand(
                     "INSERT INTO ItemDescription VALUES ('" + size + "'" + "," + "'" + itemUse + "'" + "," + "'" + itemDisc + "'" + "," + "'" + refrigType + "'" + "," + "'" + inflamType + "'" + "," + "'" + Session["dispTemp_AddForm"] + "'" + "," + "(SELECT ItemName from Inventory WHERE ItemName='" + Session["ItemNameTemp_AddForm"] + "'))", connection);

                    command.ExecuteNonQuery();
                }
                else if (itemDisc.Length > 0)
                {
                    MySqlCommand command = new MySqlCommand(
                    "INSERT INTO ItemDescription (ItemUse,OtherInfo,Refrigerated,Inflammable,Disposable,Inventory_ItemName) VALUES ('" + itemUse + "'" + "," + "'" + itemDisc + "'" + "," + "'" + refrigType + "'" + "," + "'" + inflamType + "'" + "," + "'" + Session["dispTemp_AddForm"] + "'" + "," + "(SELECT ItemName from Inventory WHERE ItemName='" + Session["ItemNameTemp_AddForm"] + "'))", connection);

                    command.ExecuteNonQuery();
                }
                else if (!size.Equals("null"))
                {
                    MySqlCommand command = new MySqlCommand(
                    "INSERT INTO ItemDescription (Size,ItemUse,Refrigerated,Inflammable,Disposable,Inventory_ItemName) VALUES ('" + size + "'" + "," + "'" + itemUse + "'" + "," + "'" + refrigType + "'" + "," + "'" + inflamType + "'" + "," + "'" + Session["dispTemp_AddForm"] + "'" + "," + "(SELECT ItemName from Inventory WHERE ItemName='" + Session["ItemNameTemp_AddForm"] + "'))", connection);

                    command.ExecuteNonQuery();
                }
                else
                {
                    TempData["notice"] = "Error: Unknown. Please Contanct the System Admins";
                    return View("AddItemPage2");
                }
                
                MySqlCommand command2 = new MySqlCommand("INSERT INTO mydb.Cost VALUES('" + cost + "', (SELECT Inventory_ItemName from ItemDescription WHERE Inventory_ItemName= '" + Session["ItemNameTemp_AddForm"] + "'))", connection);
                command2.ExecuteNonQuery();

                TempData["notice"] = "Item Has Been Added";
                return View("AddItemSuccess");
            }
        }

        public ActionResult CheckOutForm(String ItemID, String Room)
        {
            if (ItemID.Length == 0)
            {
                TempData["notice"] = "Error: Please Enter Item ID";
                return View("ViewInventoryReport");
            }
            if (ItemID.Length != 3)
            {
                TempData["notice"] = "Error: Please Enter a Item ID With 3 Digits";
                return View("ViewInventoryReport");
            }
            if (Room.Length == 0)
            {
                TempData["notice"] = "Error: Please Enter RoomNumber";
                return View("ViewInventoryReport");
            }
            if (Room.Length != 4)
            {
                TempData["notice"] = "Error: Please Enter a Room Number With 4 Characters";
                return View("ViewInventoryReport");
            }
            for (int k = 0; k < ItemID.Length; k++)
            {
                if (ItemID[k] >= '0' && ItemID[k] <= '9')
                {

                }
                else
                {
                    if (ItemID.Length != 3)
                        TempData["notice"] = "Error: Invalid Item ID and Item ID needs to be 3 digits.";
                    else
                        TempData["notice"] = "Error: Invalid Item ID";
                    return View("ViewInventoryReport");
                }
            }
            if (Room[0] >= 'A' && Room[0] <= 'E')
            {

            }
            else
            {
                TempData["notice"] = "Error: Invalid Room Number";
                return View("ViewInventoryReport");
            }
            for (int k = 1; k < Room.Length; k++)
            {
                if (Room[k] >= '0' && Room[k] <= '9')
                {

                }
                else
                {
                    TempData["notice"] = "Error: Invalid Room Number";
                    return View("ViewInventoryReport");
                }
            }

            //This reorders Items and updates the disposable items with the new value. This needs to be updated.
            MySqlConnection connection = new MySqlConnection("server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb");
            connection.Open();
            String ItemName = "";
            String Disp = "";
            String quantity = "0";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(
                  "SELECT Inventory.ItemName, Inventory.Disposable FROM Inventory WHERE Inventory.ItemID = " + ItemID, connection);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    ItemName += reader[0];
                    Disp += reader[1];
                }
                else
                {
                    TempData["notice"] = "Error: Item ID not valid";
                    return View("ViewInventoryReport");
                }
                reader.Close();

                command = new MySqlCommand(
                  "SELECT CheckedOut.QuantityCheckedOut FROM CheckedOut WHERE (CheckedOut.Inventory_ItemName = '" + ItemName + "') AND (CheckedOut.RoomNumber = '" + Room + "')", connection);

                reader = command.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    quantity = ""+reader[0];
                }
                else
                {
                    TempData["notice"] = "Error: Item ID not valid";
                    return View("ViewInventoryReport");
                }
                reader.Close();

                if (Disp.Equals("True"))
                {
                    command = new MySqlCommand(
                     "UPDATE CheckedOut SET CheckedOut.Deleted = 1 WHERE (CheckedOut.Inventory_ItemName = '" + ItemName + "') AND (CheckedOut.RoomNumber = '" + Room + "')", connection);
                    command.ExecuteNonQuery();
                }
                command = new MySqlCommand(
                     "UPDATE CheckedOut SET CheckedOut.CheckedOut_DateTime = NOW() WHERE (CheckedOut.Inventory_ItemName = '" + ItemName + "') AND (CheckedOut.RoomNumber = '" + Room + "')", connection);
                command.ExecuteNonQuery();
            }

            TempData["notice"] = ItemName+" Has Been Checked Out to Room Number " + Room;
            return RedirectToAction("ViewInventoryReport");
        }
    }
}
