<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SupplyManager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add Item Discription
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Item Discription</h2>
    <p>Please add the item Discription for <%= Session["ItemNameTemp_AddForm"]%></p>
    <p>* Indicates Required Fields</p>
    <% if (TempData["notice"] != null) { %>
        <p><%= Html.Encode(TempData["notice"]) %></p>
    <% } %>
    <% using(Html.BeginForm("AddFormDiscription","SupplyManager")) %>
    <% { %>
        <p>
            Size: 
              <select id="size" name="size">
              <option value="null">Please Select Size</option>
              <option value="Small">Small</option>
              <option value="Medium">Medium</option>
              <option value="Large">Large</option>
              </select>
        </p>
        <p>
            Item Use (100 Characters):* <br />
            <textarea name="itemUse" rows = "3" cols = "50" maxlength = "100"></textarea>
        </p>
        <p>
            Item Discription (200 Characters): <br />
            <textarea name="itemDisc" rows = "5" cols = "50" maxlength = "200"></textarea>
        </p>
        <p>
            Inflamable: <%= Html.CheckBox("inflam",false) %>*
        </p>
        <p>
            Refrigerated: <%= Html.CheckBox("refrig",false)%>*
        </p>
        <p>
            Cost: <%= Html.TextBox("cost") %>*
        </p>
        <input type="submit" value="Submit" />
    <% } %>


</asp:Content>
