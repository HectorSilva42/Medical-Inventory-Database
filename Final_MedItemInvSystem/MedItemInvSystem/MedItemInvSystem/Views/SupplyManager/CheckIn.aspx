<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SupplyManager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Check In Items
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        function DisplayButton() {
            //document.getElementById('submitButton').style.display = 'inherit';
            //document.getElementById('submitText').style.display = 'inherit';
            document.getElementById('TextType').innerHTML = "ID:";
        }
        function displayButton() {
            //document.getElementById('submitButton').style.display = 'inherit';
            //document.getElementById('submitText').style.display = 'inherit';
            document.getElementById('TextType').innerHTML = "Name:";
        }
    </script>

    <h2>Check In Items</h2>
    <% if (TempData["notice"] != null) { %>
        <p><%= Html.Encode(TempData["notice"]) %></p>
    <% } %>
    <% using (Html.BeginForm("CheckInItem", "SupplyManager")) %>
    <% { %>
        <p>
            ID: <%= Html.RadioButton("choice", "1", true, new { onclick = "DisplayButton();" })%>
            <br />
            Name: <%= Html.RadioButton("choice", "2", false, new { onclick = "displayButton();" })%>
        </p>
        <p id="submitText">
           <label id="TextType">ID:</label><%=Html.TextBox("item") %>
        </p>
        <p>
            Enter Surgery ID: <%= Html.TextBox("surgID") %>
        </p>
        <p>
            Quantity Being Returned: <%= Html.TextBox("quantity") %><br />
            <font size="1">All Items Used Must Be Returned at the Same Time</font>
        </p>
        <input type="submit" id="submitButton" value="Submit" style="display: inherit"/>
    <% } %>

</asp:Content>
