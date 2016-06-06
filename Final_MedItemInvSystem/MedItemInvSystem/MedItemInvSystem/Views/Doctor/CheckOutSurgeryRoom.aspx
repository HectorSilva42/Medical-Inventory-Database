<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Doctor.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Team 13 - Check Out Surgery Room
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Check Out Surgery Room</h2>


    <% if (TempData["notice"] != null) { %>
        <p><%= Html.Encode(TempData["notice"]) %></p>
    <% } %>

    <% using(Html.BeginForm("CheckOutRoomForm","Doctor")) %>
    <% { %>
        <p>
            Assign a Surgery ID: <%= Html.TextBox("surgeryID") %>
        </p>
        <p>
            Room #: <%= Html.TextBox("roomNumber") %>
        </p>
        <p>
            Employee ID: <%= Html.TextBox("employeeID") %>
        </p>
        <p>
            Allergens: <%= Html.TextBox("roomAllergens") %>
        </p>
        <input type="submit" value="Submit" />
    <% } %>

</asp:Content>
