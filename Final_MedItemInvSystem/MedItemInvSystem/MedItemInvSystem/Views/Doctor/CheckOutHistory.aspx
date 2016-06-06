<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Doctor.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Team 13 - Check Out History
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Check Out History</h2>
    
    <p>Items Checked Out</p>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="MySQLData" BackColor="#DEBA84" 
            BorderColor="#DEBA84" BorderWidth="1px" CellPadding="3" BorderStyle="None" 
            CellSpacing="2">
            <Columns>
                <asp:BoundField DataField="Check Out #" HeaderText="Check Out #" 
                    SortExpression="Check Out #" />
                <asp:BoundField DataField="Room #" HeaderText="Room #" />
                <asp:BoundField DataField="Item Name" HeaderText="Item Name" />
                <asp:BoundField DataField="Quantity Checked Out" HeaderText="Quantity Checked Out" />
                <asp:BoundField DataField="Checked Back In" HeaderText="Checked Back In" />
                <asp:BoundField DataField="Check Out Date" HeaderText="Check Out Date" />
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
            ConnectionString="server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb;Convert Zero Datetime=True" 
            ProviderName="MySql.Data.MySqlClient"
            SelectCommand="SELECT CheckedOut.IndexKey AS 'Check Out #', CheckedOut.RoomNumber AS 'Room #', CheckedOut.Inventory_ItemName AS 'Item Name', CheckedOut.QuantityCheckedOut AS 'Quantity Checked Out', CheckedOut.Deleted AS 'Checked Back In', CheckedOut.CheckedOut_DateTime AS 'Check Out Date'
                            FROM CheckedOut
                            WHERE CheckedOut.Personnel_EmployeeID = @SessionVar
                            ORDER BY CheckedOut.IndexKey asc">
            <SelectParameters><asp:SessionParameter Name="SessionVar" SessionField="EmployeeID" ConvertEmptyStringToNull="true" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    <br />
    <p>Rooms Checked Out</p>
    <div>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            DataSourceID="MySQLData1" BackColor="#DEBA84" 
            BorderColor="#DEBA84" BorderWidth="1px" CellPadding="3" BorderStyle="None" 
            CellSpacing="2">
            <Columns>
                <asp:BoundField DataField="Surgery ID" HeaderText="Surgery ID" 
                    SortExpression="Surgery ID" />
                <asp:BoundField DataField="Room #" HeaderText="Room #" />
                <asp:BoundField DataField="Room Allergens" HeaderText="Room Allergen(s)" />
                <asp:BoundField DataField="Checked Back In" HeaderText="Checked Back In" />
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
        <asp:SqlDataSource runat="server" ID="MySqlData1"
            ConnectionString="server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb" 
            ProviderName="MySql.Data.MySqlClient"
            SelectCommand="SELECT SurgeryRoom.SurgeryID AS 'Surgery ID', SurgeryRoom.RoomNumber AS 'Room #', SurgeryRoom.RoomAllergens AS 'Room Allergens', SurgeryRoom.Deleted AS 'Checked Back In'
                            FROM SurgeryRoom
                            WHERE SurgeryRoom.EmployeeID = @SessionVar1
                            ORDER BY SurgeryRoom.SurgeryID asc">
            <SelectParameters><asp:SessionParameter Name="SessionVar1" SessionField="EmployeeID" ConvertEmptyStringToNull="true" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>

</asp:Content>
