<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SupplyManager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Increase Item Stock
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2>Add to Existing Items</h2>

    <form id="form1" runat="server">
    <div>
    <p>Item Sheet</p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="MySQLData" BackColor="#DEBA84" 
            BorderColor="#DEBA84" BorderWidth="1px" CellPadding="3" BorderStyle="None" 
            CellSpacing="2">
            <Columns>
                <asp:BoundField DataField="Item ID" HeaderText="Item ID" />
                <asp:BoundField DataField="Item Name" HeaderText="Item Name" />
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
            SelectCommand="SELECT Inventory.ItemName AS 'Item Name', Inventory.ItemID AS 'Item ID'
                            FROM Inventory
                            ORDER BY  Inventory.ItemID asc"></asp:SqlDataSource>
    </div>
    </form>

    <% if (TempData["notice"] != null) { %>
        <%= Html.Encode(TempData["notice"]) %>
    <% } %>

    <% using (Html.BeginForm("RestockForm", "SupplyManager")) %>
    <% { %>
        <p>
            Item ID: <%= Html.TextBox("ItemID")%>
        </p>
        <p>
            Quantity to Add: <%=Html.TextBox("Quantity")%>
        </p>
        <input type="submit" value="Submit" />
    <% } %>
    <p><%: ViewData["Order"] %></p>

</asp:Content>
