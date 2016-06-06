<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Doctor.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Inventory
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Inventory</h2>

     <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="MySQLData" BackColor="#DEBA84" 
            BorderColor="#DEBA84" BorderWidth="1px" CellPadding="3" BorderStyle="None" 
            CellSpacing="2">
            <Columns>
                <asp:BoundField DataField="Item Name" HeaderText="Item Name" 
                    SortExpression="Item Name" />
                <asp:BoundField DataField="Item Type" HeaderText="Item Type" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="Disposable" HeaderText="Disposable" />
                <asp:BoundField DataField="Item ID" HeaderText="Item ID" />
                <asp:BoundField DataField="Allergen" HeaderText="Allergen" />
                <asp:BoundField DataField="Size" HeaderText="Size" />
                <asp:BoundField DataField="Item Use" HeaderText="Item Use" />
                <asp:BoundField DataField="Refrigerated" HeaderText="Refrigerated" />
                <asp:BoundField DataField="Inflammable" HeaderText="Inflammable" />
                <asp:BoundField DataField="Other Info" HeaderText="Other Info" />

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
            SelectCommand="SELECT Inventory.ItemName AS 'Item Name', Inventory.ItemType AS 'Item Type', Inventory.QuantityAvailable AS 'Quantity', Inventory.Disposable AS 'Disposable', Inventory.ItemID AS 'Item ID', Inventory.Allergen AS 'Allergen', ItemDescription.Size AS 'Size', ItemDescription.ItemUse AS 'Item Use',
            ItemDescription.Refrigerated AS 'Refrigerated', ItemDescription.Inflammable AS 'Inflammable', ItemDescription.OtherInfo AS 'Other Info'
                            FROM Inventory INNER JOIN ItemDescription
                            ON Inventory.ItemName = ItemDescription.Inventory_ItemName
                            ORDER BY Inventory.ItemName asc"></asp:SqlDataSource>
    </div>
    </form>

</asp:Content>
