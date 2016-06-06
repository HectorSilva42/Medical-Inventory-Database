<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Doctor.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Team 13 - Check In Surgery Room
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Check In Surgery Room</h2>

    <% if (TempData["notice"] != null) { %>
        <p><%= Html.Encode(TempData["notice"]) %></p>
    <% } %>

    <% using(Html.BeginForm("CheckInRoomForm","Doctor")) %>
    <% { %>
        <p>
            Surgery ID: <%= Html.TextBox("surgeryID") %>
        </p>
        <p>
            Room #: <%= Html.TextBox("roomNumber") %>
        </p>
        <p>
            Employee ID: <%= Html.TextBox("employeeID") %>
        </p>
        <input type="submit" value="Submit" />
    <% } %>

</asp:Content>
