<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SupplyManager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	View Needed Items
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>View Needed Items</h2>

     <form id="form1" runat="server" >
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="MySQLData" BackColor="#DEBA84" 
            BorderColor="#DEBA84" BorderWidth="1px" CellPadding="3" BorderStyle="None" 
            CellSpacing="2">
            <Columns>
                <asp:BoundField DataField="When" HeaderText="Date Needed" SortExpression="When" />
                <asp:BoundField DataField="What" HeaderText="Item Needed" />
                <asp:BoundField DataField="WhatID" HeaderText="Item ID" />
                <asp:BoundField DataField="HowMany" HeaderText="Quantity" />
                <asp:BoundField DataField="Who" HeaderText="Doctor" />
                <asp:BoundField DataField="Where" HeaderText="Room Number" />
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="#696969" />
            <PagerStyle ForeColor="#8C4510" 
                HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" ForeColor="White" Font-Bold="True" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView>
        <asp:SqlDataSource runat="server" ID="MySQLData"
            ConnectionString="server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb" 
            ProviderName="MySql.Data.MySqlClient"
            SelectCommand="SELECT CheckedOut.Inventory_ItemName AS 'What', CheckedOut.SurgeryDate AS 'When', Personnel.LastName AS 'Who', CheckedOut.RoomNumber AS 'Where', CheckedOut.QuantityCheckedOut AS 'HowMany', Inventory.ItemID AS 'WhatID'
                            FROM CheckedOut INNER JOIN Personnel  INNER JOIN Inventory
                            WHERE CheckedOut.Personnel_EmployeeID = Personnel.EmployeeID AND CAST(CheckedOut.SurgeryDate AS DATETIME) BETWEEN DATE_SUB(NOW(),INTERVAL 1 DAY) AND DATE_ADD(NOW(),INTERVAL 4 DAY) AND CheckedOut.Deleted = '0' AND Inventory.ItemName = CheckedOut.Inventory_ItemName
                            ORDER BY CheckedOut.SurgeryDate asc"></asp:SqlDataSource>
    </div>
    </form>

    <% if (TempData["notice"] != null) { %>
        <p><%= Html.Encode(TempData["notice"]) %></p>
    <% } %>

    <% using (Html.BeginForm("CheckOutForm", "SupplyManager")) %>
    <% { %>
        <p>
            Item ID: <%= Html.TextBox("ItemID")%>
        </p>
        <p>
            Room Number: <%=Html.TextBox("Room")%>
        </p>
        <input type="submit" value="Submit" />
    <% } %>
</asp:Content>
