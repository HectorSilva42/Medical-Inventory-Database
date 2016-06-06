<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    MI-13
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id = "message", align="center">
        <h2><%: ViewData["Message"] %></h2>
        <p>
            Welcome to the medical inventory system for the Team 13 Medical Center.
        </p>
        <p>
            Please <%:Html.ActionLink("Log In", "LogOn2", "Home") %> to check out or manage items.
        </p>
        <p>
            If you are a visitor to the site, please feel free to go to the <%: Html.ActionLink("About", "About", "Home") %> page to learn more about us.
        </p>
    </div>
</asp:Content>
