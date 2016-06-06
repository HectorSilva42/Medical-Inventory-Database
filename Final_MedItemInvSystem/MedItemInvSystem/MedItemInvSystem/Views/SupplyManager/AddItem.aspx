<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SupplyManager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add Item to Inventory
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Item</h2>
     <% if (TempData["notice"] != null) { %>
        <p><%= Html.Encode(TempData["notice"]) %></p>
    <% } %>

    <% using(Html.BeginForm("AddForm","SupplyManager")) %>
    <% { %>
        <p>
            Item Name: <%= Html.TextBox("itemName") %>
        </p>
        <p>
            Item ID#: <%= Html.TextBox("itemID") %>
        </p>
        <p>
            Item Type: <%= Html.TextBox("itemType")%>
        </p>
        <p>
            Disposable: <%= Html.CheckBox("disp") %>
        </p>
        <p>
            Quantity: <%= Html.TextBox("quantity")%>
        </p>
        <p>
            Allergen: <%= Html.TextBox("allergen") %>
        </p>
        <input type="submit" value="Submit" />
    <% } %>

</asp:Content>
