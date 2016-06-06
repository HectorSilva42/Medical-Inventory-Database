<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Nurse.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CheckOutItem
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Check Out Item</h2>

    <% if (TempData["notice"] != null) { %>
        <p><%= Html.Encode(TempData["notice"]) %></p>
    <% } %>

    <% using(Html.BeginForm("CheckOutItemForm","Nurse")) %>
    <% { %>
        <p>
            Item Name: <%= Html.TextBox("itemName") %>
        </p>
        <p>
            Item ID: <%= Html.TextBox("id") %>
        </p>
        <p>
            Employee ID: <%= Html.TextBox("employeeID") %>
        </p>
        <p>
            Surgery Date: <%= Html.TextBox("date") %>
        </p>
        <p>
            Surgery ID: <%= Html.TextBox("surgeryID") %>
        </p>
        <p>
            Room: <%= Html.TextBox("room") %>
        </p>
        <p>
            Quantity: <%= Html.TextBox("quantity") %>
        </p>
        <input type="submit" value="Submit" />
    <% } %>

</asp:Content>
