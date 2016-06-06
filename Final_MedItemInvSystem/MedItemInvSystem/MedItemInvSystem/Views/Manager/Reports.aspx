<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reports
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2> 
        Below are List of Reports:
    </h2>

        <div class="hlist" id="menucontainer">
                             
                    <li><%: Html.ActionLink("List of Items Available", "Inventory", "Manager") %></li>
                    <li> </li>
                    <li><%: Html.ActionLink("List of Users", "ListOfUsers", "Manager") %></li>
                    <li> </li>
                    <li><%: Html.ActionLink("Check Out History", "CheckOutHistory", "Manager") %></li>
                    <li> </li>
                    <li><%: Html.ActionLink("List of Surgery Rooms", "SurgeryRoom", "Manager") %></li>
                    <li> </li>
                    <li><%: Html.ActionLink("Totals for Check Out History", "TotalCheckOut", "Manager") %></li>
            </div>

</asp:Content>
