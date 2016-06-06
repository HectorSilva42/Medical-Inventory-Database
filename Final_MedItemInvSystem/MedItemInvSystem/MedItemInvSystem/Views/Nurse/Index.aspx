<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Nurse.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Team 13 Inventory System - Nurse
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Nurse</h2>
    <p>Welcome Back <%= Session["id"] %>.</p>
    <p>Using this page, the Nurse can check out items.</p>

</asp:Content>
