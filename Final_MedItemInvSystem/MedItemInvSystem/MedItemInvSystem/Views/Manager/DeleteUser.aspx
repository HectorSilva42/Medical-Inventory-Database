<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Team 13 - Delete User
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete A User</h2>

    <% if (TempData["notice"] != null) { %>
        <p><%= Html.Encode(TempData["notice"]) %></p>
    <% } %>

    <% using(Html.BeginForm("DeleteForm","Manager")) %>
    <% { %>
        Employee ID: <%= Html.TextBox("id") %>
        <input type="submit" value="Submit" />
    <% } %>

</asp:Content>
