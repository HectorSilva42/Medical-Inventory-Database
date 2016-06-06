<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Team 13 - Add User
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add A User</h2>

    <% if (TempData["notice"] != null) { %>
        <p><%= Html.Encode(TempData["notice"]) %></p>
    <% } %>

    <% using(Html.BeginForm("AddForm","Manager")) %>
    <% { %>
        <p>
            First Name: <%= Html.TextBox("first") %>
        </p>
        <p>
            Middle Initial: <%= Html.TextBox("middle") %>
        </p>
        <p>
            Last Name: <%= Html.TextBox("last") %>
        </p>
        <p>
            Employee ID: <%= Html.TextBox("id") %>
        </p>
        <p>
            Password: <%= Html.Password("password") %>
        </p>
        <p>
            Classification: <%= Html.TextBox("classification") %>
        </p>
        <input type="submit" value="Submit" />
    <% } %>

</asp:Content>
