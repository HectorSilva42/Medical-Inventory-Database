<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Nurse.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Team 13 - Check In Item
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Check In Item</h2>

    <% if (TempData["notice"] != null) { %>
        <p><%= Html.Encode(TempData["notice"]) %></p>
    <% } %>

    <% using(Html.BeginForm("CheckInItemForm","Nurse")) %>
    <% { %>
        <p>
            Check Out #: <%= Html.TextBox("checkOutNum") %>
        </p>
        <p>
            Item Name: <%= Html.TextBox("itemName") %>
        </p>
        <p>
            Item ID: <%= Html.TextBox("id") %>
        </p>
        <p>
            Quantity: <%= Html.TextBox("quantity") %>
        </p>
        <input type="submit" value="Submit" />
    <% } %>

</asp:Content>
