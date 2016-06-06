<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Doctor.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Team 13 Inventory System - Doctor
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Doctor</h2>
    <p>Welcome Back <%= Session["id"] %>.</p>
    <p> Using this page, the Doctor can check out items and manage surgery rooms. </p>

</asp:Content>
