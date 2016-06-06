<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SurgeryRoom
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Surgery Room Report</h2>

    <% using(Html.BeginForm("ListOfRoomsForm","Manager")) %>
    <% { %>
        <p>
            Employee ID: <%= Html.TextBox("employeeID") %>
        </p>
        <p>
            Surgery ID: <%= Html.TextBox("surgeryID") %>
        </p>
        <p>
            Room #: <%= Html.TextBox("roomNumber") %>
        </p>
        <p>
            Checked Out <%= Html.CheckBox("checkedOut") %>
            Checked Back In <%= Html.CheckBox("back") %>
        </p>
        <p>
            Allergens:<br />
            Latex <%= Html.CheckBox("latex") %> <br />
            Plastic <%= Html.CheckBox("plastic") %> <br />
            Penicillin <%= Html.CheckBox("penicillin") %><br />
        </p>

        <input type="submit" value="Submit" />
    <% } %>

    <% using(Html.BeginForm("EmptyListOfRoomsForm","Manager")) %>
    <% { %>
        <input type="submit" value="Get All Rooms" />
    <% } %>

    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="MySQLData" BackColor="#DEBA84" 
            BorderColor="#DEBA84" BorderWidth="1px" CellPadding="3" BorderStyle="None" 
            CellSpacing="2">
            <Columns>
                <asp:BoundField DataField="Surgery ID" HeaderText="Surgery ID" 
                    SortExpression="Surgery ID" />
                <asp:BoundField DataField="Room Number" HeaderText="Room Number"/>
                <asp:BoundField DataField="Employee ID" HeaderText="Employee ID"/>
                <asp:BoundField DataField="Room Allergy" HeaderText="Room Allergy" />
                <asp:BoundField DataField="Deleted" HeaderText="Checked Back In" />                
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
            SelectCommand= "SELECT SurgeryRoom.SurgeryID AS 'Surgery ID', SurgeryRoom.RoomNumber AS 'Room Number', SurgeryRoom.EmployeeID AS 'Employee ID', SurgeryRoom.RoomAllergens AS 'Room Allergy', SurgeryRoom.Deleted AS 'Deleted', SurgeryRoom.EmployeeID AS 'Employee ID'
                            FROM SurgeryRoom
                            WHERE SurgeryRoom.SurgeryID LIKE @SessionVar2 AND SurgeryRoom.RoomNumber LIKE @SessionVar3 AND SurgeryRoom.EmployeeID LIKE @SessionVar AND SurgeryRoom.Deleted LIKE @SessionVar4 
                            AND IFNULL(SurgeryRoom.RoomAllergens, '') LIKE @SessionVar5 OR SurgeryRoom.RoomAllergens LIKE @SessionVar6 AND SurgeryRoom.SurgeryID LIKE @SessionVar2 AND SurgeryRoom.RoomNumber LIKE @SessionVar3 AND SurgeryRoom.EmployeeID LIKE @SessionVar AND SurgeryRoom.Deleted LIKE @SessionVar4 
                            ORDER BY SurgeryRoom.SurgeryID asc">
            <SelectParameters>
                    <asp:SessionParameter Name="SessionVar" SessionField="EmployeeIDSession" ConvertEmptyStringToNull="true" />
                    <asp:SessionParameter Name="SessionVar2" SessionField="SurgeryIDSession" ConvertEmptyStringToNull="true" />
                    <asp:SessionParameter Name="SessionVar3" SessionField="RoomNumberSession" ConvertEmptyStringToNull="true" />
                    <asp:SessionParameter Name="SessionVar4" SessionField="DeletedSession" ConvertEmptyStringToNull="true" />
                    <asp:SessionParameter Name="SessionVar5" SessionField="AllergenSession" ConvertEmptyStringToNull="true" />
                    <asp:SessionParameter Name="SessionVar6" SessionField="Allergen2Session" ConvertEmptyStringToNull="true" />
            </SelectParameters>                    
        </asp:SqlDataSource>
    </div>
    </form>

</asp:Content>
