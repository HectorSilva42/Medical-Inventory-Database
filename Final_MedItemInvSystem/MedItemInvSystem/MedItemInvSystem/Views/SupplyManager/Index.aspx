<%@ Page Language="C#" MasterPageFile="~/Views/Shared/SupplyManager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Team 13 Inventory System - Supply
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Supply Manager</h2>
    <p>Welcome Back <%= Session["id"] %>. Below are all Upcoming Surgeries. Please Use the Navigation at the Top of the Page</p>

    

</asp:Content>
