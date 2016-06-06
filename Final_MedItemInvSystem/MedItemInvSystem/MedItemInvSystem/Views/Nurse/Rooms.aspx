<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Nurse.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Team 13 - Rooms
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Rooms Checked Out</h2>

    <form runat="server">
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
            SelectCommand="SELECT SurgeryRoom.SurgeryID AS 'Surgery ID', SurgeryRoom.RoomNumber AS 'Room #', SurgeryRoom.RoomAllergens AS 'Room Allergens'
                            FROM SurgeryRoom
                            WHERE SurgeryRoom.Deleted = 0
                            ORDER BY SurgeryRoom.SurgeryID asc">
            <SelectParameters><asp:SessionParameter Name="SessionVar1" SessionField="EmployeeID" ConvertEmptyStringToNull="true" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>

</asp:Content>
